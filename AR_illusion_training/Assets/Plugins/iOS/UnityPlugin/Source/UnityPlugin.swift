//
//  UnityPlugin.swift
//  UnityPlugin
//
//  Created by sejin on 2023/10/25.
//

import Foundation
import CoreMotion
import AVFoundation
import HealthKit

@objc public class UnityPlugin: NSObject {
    @objc public static let shared = UnityPlugin()
    @objc public let motionManager = CMMotionManager()
    @objc public let healthStore = HKHealthStore()
    
    @objc public func AddTwoNumber(a:Int,b:Int ) -> Int {
        let result = a+b;
        return result;
    }
    
    @objc public func getString(name: String) -> String {
        return "My name is \(name)!"
    }
    
    @objc public func startTracking(objectName: String) {
        print("디버그 가속도 측정 가능 여부: \(motionManager.isAccelerometerAvailable)")
        requestHealthKitAuthorication()
        motionManager.accelerometerUpdateInterval = 0.1
        motionManager.startAccelerometerUpdates(to: .main) { data, error in
            if let error {
                print("디버그 \(error)")
            }
            
            guard let x = data?.acceleration.x,
                  let y = data?.acceleration.y,
                  let z = data?.acceleration.z else {
                return
            }
            print("x: ", x)
            print("y: ", y)
            print("z: ", z)
            let speed = sqrt(pow(x,2)) + sqrt(pow(y,2)) + sqrt(pow(z,2))
            print("디버그 가속도: \(speed)")
            
            let directionMessage = "x:\(x) y:\(y) z:\(z)"
            print("디버그 방향: \(directionMessage)")
            UnitySendMessage(objectName, "SpeedCallBackMethod", String(speed));
            UnitySendMessage(objectName, "DirectionCallBackMethod", directionMessage);
        }
    }
    
    @objc public func stopTracking() {
        motionManager.stopAccelerometerUpdates()
    }
    
    @objc public func makeVibrate() {
        AudioServicesPlaySystemSound(kSystemSoundID_Vibrate)
    }
    
    @objc public func requestHealthKitAuthorication() {
        let read = Set([HKObjectType.quantityType(forIdentifier: .heartRate)!, HKSampleType.quantityType(forIdentifier: .activeEnergyBurned)!])
        let share = Set([HKObjectType.quantityType(forIdentifier: .heartRate)!, HKSampleType.quantityType(forIdentifier: .activeEnergyBurned)!])
        
        self.healthStore.requestAuthorization(toShare: share, read: read) { success, error in
            if let error { print(error) }
            
            print("디버그 헬스킷 권한 성공 유무: \(success)")
        }
    }
    
    @objc public func getEnergeBurned(objectName: String) {
        print("디버그 오늘 소모한 칼로리 측정")
        
        guard let activityEnergyBurendType = HKSampleType.quantityType(forIdentifier: .activeEnergyBurned) else { return }
        
        let now = Date()
        let startDate = Calendar.current.startOfDay(for: now)
        let predicate = HKQuery.predicateForSamples(withStart: startDate, end: now, options: .strictStartDate)
        
        let query = HKStatisticsQuery(quantityType: activityEnergyBurendType, quantitySamplePredicate: predicate, options: .cumulativeSum) { _, result, error in
            if let error { print("디버그 칼로리 측정 에러: \(error)") }
            
            var cal: Double = 0
            
            guard let result = result, let sum = result.sumQuantity() else {
                print("칼로리 소모 측정 실패")
                return
            }
            
            cal = sum.doubleValue(for: HKUnit.kilocalorie())
            print("디버그 칼로리: \(cal)")
            
            UnitySendMessage(objectName, "CalorieCallBackMethod", String(cal));
        }
        
        healthStore.execute(query)
    }
    
    @objc public func getHeartRateData(objectName: String) {
        guard let sampleType = HKObjectType.quantityType(forIdentifier: .heartRate) else { return }
        
        let startDate = Calendar.current.date(byAdding: .hour, value: -1, to: Date())
        
        let predicate = HKQuery.predicateForSamples(withStart: startDate, end: Date(), options: .strictEndDate)
        
        let sortDescriptor = NSSortDescriptor(key: HKSampleSortIdentifierStartDate, ascending: true)
        
        let query = HKSampleQuery(sampleType: sampleType, predicate: predicate
                                  , limit: Int(HKObjectQueryNoLimit),
                                  sortDescriptors: [sortDescriptor]) { sample, result, error in
            if let error {
                print("디버그 심장 박동 측정 에러: \(error)")
            }
            
            guard let result = result as? [HKQuantitySample] else {
                print("심장 박동 측정 실패")
                return
            }
            
            let heartRate = result.map { $0.quantity.doubleValue(for: HKUnit(from: "count/min")) }.map { String($0) }
            
            let heartRateString = heartRate.joined(separator: " ")
            
            print("디버그 심장 박동: \(heartRateString)")
            
            UnitySendMessage(objectName, "HeartRateCallBackMethod", heartRateString);
        }
        
        healthStore.execute(query)
    }
}

