<?php
include("config.inc");
$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

$k = $_GET["k"];
if ($k == "") $k = 0;
$o = $_GET["o"];
if ($o == "") $o = 0;
$KIOSKID = $k;
$ORDERNUMBER = $o;

$s .= "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<IOORDER>\n";

$s .= "</IOORDER>\n";
echo $s;

ibase_close($dbh);
?>