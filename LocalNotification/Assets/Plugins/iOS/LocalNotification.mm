// iOS10以上でもUILocalNotificationを使うならインポート.
// ※ Frameworkも追加する.
#import <UserNotifications/UserNotifications.h>

extern "C" {
    void Initialize();
    void AddLocalNotification(int notificationId, char *title, char *msg, int interval);
    void RemovePendingLocalNotification(int notificationId);
    void ClearLocalNotification();
}

void Initialize()
{
    // iOS8以上であれば、通知許可の可否を問う.
    if(floor( NSFoundationVersionNumber ) >= NSFoundationVersionNumber_iOS_8_0)
    {
        // バッジ、サウンド、通知OK？.
        UIUserNotificationType types = UIUserNotificationTypeBadge | UIUserNotificationTypeSound | UIUserNotificationTypeAlert;
        UIUserNotificationSettings *setting = [UIUserNotificationSettings settingsForTypes:types categories:nil];
        // 設定ダイアログ.
        [[UIApplication sharedApplication] registerUserNotificationSettings:setting];
    }
}

// ローカル通知登録.
void AddLocalNotification(int notificationId, char *title, char *msg, int interval)
{
    // タイトルとメッセージをStringに変換.
    NSString *str_title = [NSString stringWithCString: title encoding:NSUTF8StringEncoding];
    NSString *str_msg   = [NSString stringWithCString: msg encoding:NSUTF8StringEncoding];
    NSString *str_id    = [NSString stringWithFormat: @"%d", notificationId];
    
    // iOS10以上であれば、UILocalNotificationを使わない.
    if ( floor(NSFoundationVersionNumber) >= NSFoundationVersionNumber10_0 )
    {
        // 初期化.
        UNMutableNotificationContent *objNotificationContent = [[UNMutableNotificationContent alloc] init];
        // 表示メッセージを設定.
        objNotificationContent.body     = str_msg;
        objNotificationContent.title    = str_title;
        // アイコンにつくバッジ数を設定.
        //        objNotificationContent.badge    = @([[UIApplication sharedApplication] applicationIconBadgeNumber] + 1);
        objNotificationContent.badge     = 0;
        // 表示する日時を設定.
        UNTimeIntervalNotificationTrigger *trigger = [UNTimeIntervalNotificationTrigger triggerWithTimeInterval:interval repeats:NO];
        // 通知リクエストを作成.
        UNNotificationRequest *request      = [UNNotificationRequest requestWithIdentifier:str_id content:objNotificationContent trigger:trigger];
        UNUserNotificationCenter *center    = [UNUserNotificationCenter currentNotificationCenter];
        // 通知センターに登録.
        [center addNotificationRequest:request withCompletionHandler:^(NSError * _Nullable error) {
            if (error) {
                // TODO エラー発生時の処理.
            }
            else {
                // TODO 正常終了時の処理.
            }
        }];
    }
    else
    {
        // 初期化.
        UILocalNotification *localNotification  = [[UILocalNotification alloc] init];
        // 表示する日時を設定.
        localNotification.fireDate              = [[NSDate date] dateByAddingTimeInterval:interval];
        // 表示メッセージを設定.
        localNotification.alertTitle            = str_title;
        localNotification.alertBody             = str_msg;
        //        localNotification.repeatInterval        = NSCalendarUnitMinute;
        // アイコンにつくバッジ数を設定.
        localNotification.applicationIconBadgeNumber = 0;
        // ID指定.
        localNotification.userInfo = @{ @"id": str_id };
        
        // 通知スケジュール登録.
        [[UIApplication sharedApplication] scheduleLocalNotification:localNotification];
    }
}

// 指定IDの通知を削除.
void RemovePendingLocalNotification(int notificationId)
{
    NSString *str_id    = [NSString stringWithFormat: @"%d", notificationId];
    
    if( floor(NSFoundationVersionNumber) >= NSFoundationVersionNumber10_0 )
    {
        NSArray *identifiers = @[str_id];
        
        UNUserNotificationCenter *center = [UNUserNotificationCenter currentNotificationCenter];
        [center removePendingNotificationRequestsWithIdentifiers:identifiers];
    }
    else
    {
        UILocalNotification *removeNotification = nil;
        for (UILocalNotification *aNotification in [[UIApplication sharedApplication] scheduledLocalNotifications]) {
            if([[aNotification.userInfo objectForKey:@"id"] isEqualToString:str_id]){
                removeNotification = aNotification;
                break;
            }
        }
        if(removeNotification)[[UIApplication sharedApplication] cancelLocalNotification:removeNotification];
    }
    
}

// 登録しているローカル通知を全削除.
void ClearLocalNotification()
{
    if( floor(NSFoundationVersionNumber) >= NSFoundationVersionNumber10_0 )
    {
        UNUserNotificationCenter *center = [UNUserNotificationCenter currentNotificationCenter];
        // 届いた通知を全削除する.
        //        [center removeAllDeliveredNotifications];
        // 届いていない通知を全削除する.
        [center removeAllPendingNotificationRequests];
    }
    else
    {
        [[UIApplication sharedApplication] cancelAllLocalNotifications];
    }
}

