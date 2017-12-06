# ローカル通知

# Android 
1. Notficaition_AndroidProjectからAndroidStudioで localnotification.aar をビルドする  
`./gradle build`
2. 生成したaarを Plugins/Android へインポートする
3. AndroidManifest.xml に以下を追加  
`<receiver android:name="com.mobcast.localnotification.LocalNotificationReceiver" />`
4. Assets/Plugins/LocalNotificationPlugin.cs　を Plugins へインポートする　　
5. 起動時に `LocalNotificationPlugin.Init()` を実行する　　
6. 通知を登録する場合は、`LocalNotificationPlugin.Add(id, title, msg, interval)` を実行する　　
7. 通知を削除する場合は、`LocalNotificationPlugins.Remove(id)` を実行する　　

# iOS
1. Assets/Plugins/iOS/LocalNotification.mm を Plugins/iOS へインポートする　　
2. Assets/Plugins/LocalNotificationPlugin.cs　を Plugins へインポートする
3. 起動時に `LocalNotificationPlugin.Init()` を実行する　　
4. 通知を登録する場合は、`LocalNotificationPlugin.Add(id, title, msg, interval)` を実行する　　
5. 通知を削除する場合は、`LocalNotificationPlugins.Remove(id)` を実行する　　
6. Xcodeにて、 UserNotifications.framework を追加　　　
