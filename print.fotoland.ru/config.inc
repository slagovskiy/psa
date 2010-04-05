<?php
// SNYATO.RU SERVER PART CONFIG


// MAIN DB PROPERTIES
define('DB', '127.0.0.1:/base/PHOTO.GDB');
define('DBUSER', 'SYSDBA');
define('DBPASSWD', 'masterkey');

// PATHS
define('INETORDERPATH', '/share/orders');
define('INTERNETORDERPATH', '/share/orders');
define('ORDERPATH', '/share/KIOSK_NET');
define('LINKPATH', '/share/to_print');
define('DBBAKPATH', '/share/db_bak');
define('KIOSKUPDATESPATH', '/share/update/');

// DEFAULT INTERNET PRICE
define('DEFAULTPRICE', 19);

// DISCOUNTS INTERNET
define('DISCOUNT1','0.1');
define('DISCOUNT2','0.2');
define('DISCOUNT_BORDER1','250');
define('DISCOUNT_BORDER2','500');


// ENCODINGS
define('SRCENCODING','windows-1251');
define('DSTENCODING','UTF-8');

//UTF-8');

//koi8-r');

//windows-1251');

// WALLETS 
define('WALLET_SECRET','1234');

// VARIOUS
define('TECHSMS','79089040272@sms.ycc.ru');
define('DEFAULTDEST','http://online.snyato.ru');

?>
