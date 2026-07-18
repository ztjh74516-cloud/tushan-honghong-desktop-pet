# 涂山红红桌宠

一个基于 .NET 8 / WPF 的 Windows 桌宠。当前 `feature/core-player` 分支提供首个可运行的核心播放器：透明置顶窗口、待机动画播放、拖拽、位置锁定、系统托盘控制与位置持久化。

## 运行环境

- Windows 10 或更高版本
- .NET 8 SDK（开发或从源码运行时需要）

## 从源码启动

在项目根目录执行：

```powershell
dotnet restore
dotnet run --project src/TushanHonghong.DesktopPet
```

运行后，宠物会显示在桌面上；右键系统托盘图标可以隐藏、恢复、锁定位置、召回到屏幕中央或退出。

## 资产说明

播放器从 `src/TushanHonghong.DesktopPet/Assets/base/animations.json` 读取动画声明。当前原型资产仅用于技术验证；后续会替换为独立制作的、无狐火特效的涂山红红动画资产，不会修改 Codex 内置宠物文件。

## 验证

```powershell
dotnet test TushanHonghong.DesktopPet.sln
```
