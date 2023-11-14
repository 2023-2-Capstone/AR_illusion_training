//
//  UnityPlugin.swift
//  UnityPlugin
//
//  Created by sejin on 2023/10/25.
//

import Foundation
import CoreMotion

@objc public class UnityPlugin: NSObject {
    @objc public static let shared = UnityPlugin()
    @objc public let motionManager = CMMotionManager()
    
    @objc public func AddTwoNumber(a:Int,b:Int ) -> Int {
        let result = a+b;
        return result;
    }
    
    @objc public func getString(name: String) -> String {
        return "My name is \(name)!"
    }
    
    @objc public func startTracking(objectName: String) {
        print("디버그 가속도 측정 가능 여부: \(motionManager.isAccelerometerAvailable)")
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
            UnitySendMessage(objectName, "SpeedCallBackMethod", String(speed));
        }
    }
    
    @objc public func stopTracking() {
        motionManager.stopAccelerometerUpdates()
    }
}
