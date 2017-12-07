# ローカル通知

# Android 
1. Notficaition_AndroidProjectからAndroidStudioで localnotification.aar をビルドする  
`./gradle build`
2. 生成したaarを Plugins/Android へインポートする
3. AndroidManifest.xml に以下を追加  
`<receiver android:name="com.mobcast.localnotification.LocalNotificationReceiver" />`
4. [Assets/Plugins/LocalNotificationPlugin.cs](https://github.com/mob-kataoka/local_notification/blob/develop/LocalNotification/Assets/Plugins/LocalNotificationPlugin.cs)　を Plugins へインポートする　　

# iOS
1. [Assets/Plugins/iOS/LocalNotification.mm](https://github.com/mob-kataoka/local_notification/blob/develop/LocalNotification/Assets/Plugins/iOS/LocalNotification.mm) を Plugins/iOS へインポートする　　
2. [Assets/Plugins/LocalNotificationPlugin.cs](https://github.com/mob-kataoka/local_notification/blob/develop/LocalNotification/Assets/Plugins/LocalNotificationPlugin.cs)　を Plugins へインポートする
3. Xcodeにて、 UserNotifications.framework を追加　　　

# Unity
1. 起動時に `LocalNotificationPlugin.Init()` を実行する　　
2. 通知を登録する場合は、`LocalNotificationPlugin.Add(id, title, msg, interval)` を実行する　　
3. 通知を削除する場合は、`LocalNotificationPlugins.Remove(id)` を実行する　　
