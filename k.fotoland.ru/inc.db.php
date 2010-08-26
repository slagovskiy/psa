<?php
$db_host = "127.0.0.1";
$db_port = "";
$db_socket = "";
$db_database = "k";
$db_user = "k";
$db_password = "kpasswd1020";


$db = @mysql_connect($db_host . $db_port . $db_socket, $db_user, $db_password);
if (!$db) {
	echo "Error: connection mysql server!";
	exit();
} else {
	if (!@mysql_select_db($db_database, $db)){
		echo "Error: open database!";
		exit();
	}
}
mysql_query("SET NAMES cp1251");
?>