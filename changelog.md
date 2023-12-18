# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-purchasing/releases/tag/2.0.0-preview.2) - 2023-12-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-purchasing/milestone/4?closed=1)  
    

### Changed

- Update dependencies ([#7](https://github.com/unity-game-framework/ugf-module-purchasing/issues/7))  
    - Update dependencies: `com.ugf.module.services` to `1.0.0-preview.2` and `com.unity.purchasing` to `4.10.0` versions.
    - Update package _Unity_ version to `2023.2`.
    - Update package registry to _UPM Hub_.
    - Change `PurchaseUnityStore` class implementation to support latest _Unity_ purchasing API changes.

## [2.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-purchasing/releases/tag/2.0.0-preview.1) - 2023-04-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-purchasing/milestone/3?closed=1)  
    

### Changed

- Update dependencies ([#5](https://github.com/unity-game-framework/ugf-module-purchasing/issues/5))  
    - Change dependencies: `com.unity.purchasing` to `4.7.0` and add `com.ugf.module.services` of `1.0.0-preview.1` version.
    - Update package _Unity_ version to `2022.2`.
    - Change `IPurchaseModule.Products` collection implementation.
    - Change `PurchaseProductDescription` and `PurchaseModuleDescription` classes to have constructor.
    - Fix `PurchaseProduct.Available` property was missing in constructor.
    - Fix `PurchaseProductId` structure validation method.
    - Fix `PurchaseUnityStore.OnInitializeFailed()` method implementation was missing.

## [2.0.0-preview](https://github.com/unity-game-framework/ugf-module-purchasing/releases/tag/2.0.0-preview) - 2022-07-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-purchasing/milestone/2?closed=1)  
    

### Changed

- Change string ids to global id data ([#3](https://github.com/unity-game-framework/ugf-module-purchasing/issues/3))  
    - Update dependencies: `com.ugf.application` to `8.3.0`, `com.ugf.editortools` to `2.8.1` and `com.unity.purchasing` to `4.2.1` versions.
    - Update package _Unity_ version to `2022.1`.
    - Change usage of ids as `GlobalId` structure instead of `string`.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-module-purchasing/releases/tag/1.0.0-preview) - 2022-05-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-purchasing/milestone/1?closed=1)  
    

### Added

- Add implementation ([#1](https://github.com/unity-game-framework/ugf-module-purchasing/issues/1))  
    - Add `IPurchaseModule` interface of the module and `PurchaseUnityModule` class as _Unity IAP_ implementation.


