<?php
require_once("config.inc");

$o = $_GET["o"];
$d = $_GET["d"];
$t = $_GET["a"];

$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");
$ok=ibase_query($dbh, 'insert into ORDER_OK(ORDER_NUMBER,EVENT_DATE,EVENT_TYPE) values(?,?,?)',$o,$d,$t) or die(ibase_errmsg());
if($t == 2) {
// Для Интернет-розницы меняем на статус Выдан
	$prefix = substr($o,0,2);
	if($prefix  ==  41) {
		$order = intval(substr($o,2,10));
		$sth=ibase_query("update orders set status=? where id=?",'Выдан', $order);
	}
}

ibase_close($dbh);
echo('ok');
?>