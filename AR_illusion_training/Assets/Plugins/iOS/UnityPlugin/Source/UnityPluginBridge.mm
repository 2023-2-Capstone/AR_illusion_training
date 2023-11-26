//
//  UnityPluginBridge.m
//  UnityPlugin
//
//  Created by sejin on 2023/10/25.
//

#import <Foundation/Foundation.h>
#include "UnityFramework/UnityFramework-Swift.h"

extern "C" {

#pragma mark - Functions

    int _addTwoNumberInIOS(int a , int b) {
       
        int result = [[UnityPlugin shared] AddTwoNumberWithA:(a) b:(b)];
        return result;
    }

    const char* _getString(const char* name) {
        const char* result = (const char*)[[[UnityPlugin shared] getStringWithName:[NSString stringWithUTF8String:name]] UTF8String];
        
        char* ptrReturnValue = (char*) malloc(strlen(result) + 1);
        strcpy(ptrReturnValue, result);

        return ptrReturnValue;
    }

    void FreePointer(char* ptr) {
       free(ptr);
    }

    void _startTracking(const char* objectName) {
        [[UnityPlugin shared] startTrackingWithObjectName:[NSString stringWithUTF8String:objectName]];
    }
    
    void _stopTracking() {
        [[UnityPlugin shared] stopTracking];
    }

    void _makeVibrate() {
        [[UnityPlugin shared] makeVibrate];
    }
    
    void _getEnergeBurned(const char* objectName) {
        [[UnityPlugin shared] getEnergeBurnedWithObjectName:[NSString stringWithUTF8String:objectName]];
    }

    void _getHeartRateData(const char* objectName) {
        [[UnityPlugin shared] getHeartRateDataWithObjectName:[NSString stringWithUTF8String:objectName]];
    }
}
