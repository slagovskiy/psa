<?php
require_once("config.inc");
$o = $_GET["o"];
$d = $_GET["d"];
$t = $_GET["a"];
$dbh = ibase_connect(DB, DBUSER, DBPASSWD, "win1251");
$ok=ibase_query('insert into ORDER_OK(ORDER_NUMBER,EVENT_DATE,EVENT_TYPE) values("' . $o . '","' . $d . '",' . $t . ')');
if($ok)
	echo('ok');
else
	echo('error');
?>