# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2023-08-14

### Added

- Support inner cone angle and falloff for baking spotlights.
- Lighting settings assets for all scenes.
- __.gitignore__ file.

### Changed

- Updated Unity to version 2022.3.5f1.
- Upgraded __Core RP Library__ package to version 14.0.8 and removed AI package.
- Switched to `CustomRenderPipeline.Render` version with list instead of array parameter.
- Disabled automatic baking for __Baked Light__ scene so generated maps can be inspected.
- Provide required `BatchCullingProjectionType` argument to `DrawShadowSettings` constructor, even through it will be deprecated again in Unity 2023.
- Project _Enter Play Mode Settings_ set to not reload domain and scene.
- C# code uses more modern syntax and more closely matches standard and Unity C# style.
- Moved scenes to their own folders.
- Moved materials to a common and to scene-specific folders.

### Fixed

- Added missing matrices to __Common.hlsl__ and __UnityInput.hlsl__.
- Also clear color when skybox is used, to avoid `NaN` and `Inf` values that can mess up frame buffer blending.
- Always perform a buffer load for the final draw if using a reduced viewport, to prevent artifacts on tile-based GPUs.
- Copy depth once more after post FX, so post-FX gizmos correctly use depth.
- Normalize unpacked normal vectors because Unity no longer does this.
- Removed duplicate for-loop iterator variable declaration in __FXAAPass.hlsl__ to avoid shader compiler warning.
