using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;

namespace TushanHonghong.DesktopPet.Services;

public sealed class TrayService : IDisposable
{
    private readonly TaskbarIcon _taskbarIcon;

    public TrayService(Action hide, Action restore, Action toggleLock, Action summon, Action reload, Action exit)
    {
        _taskbarIcon = new TaskbarIcon { ToolTipText = "涂山红红桌宠" };
        var menu = new ContextMenu();
        menu.Items.Add(CreateItem("隐藏到托盘", hide));
        menu.Items.Add(CreateItem("恢复", restore));
        menu.Items.Add(CreateItem("锁定位置", toggleLock, checkable: true));
        menu.Items.Add(new Separator());
        menu.Items.Add(CreateItem("召回到屏幕中央", summon));
        menu.Items.Add(CreateItem("重新加载宠物", reload));
        menu.Items.Add(new Separator());
        menu.Items.Add(CreateItem("退出", exit));
        _taskbarIcon.ContextMenu = menu;
        _taskbarIcon.TrayMouseDoubleClick += (_, _) => restore();
    }

    public void SetLockState(bool isLocked)
    {
        if (_taskbarIcon.ContextMenu.Items[2] is MenuItem lockItem)
        {
            lockItem.IsChecked = isLocked;
        }
    }

    public void Dispose() => _taskbarIcon.Dispose();

    private static MenuItem CreateItem(string header, Action action, bool checkable = false)
    {
        var item = new MenuItem { Header = header, IsCheckable = checkable };
        item.Click += (_, _) => action();
        return item;
    }
}
