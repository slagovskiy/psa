<?php

	include("config.inc");
	$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");


	$s = "";
	$s .= "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<database>\n";

	$h = ibase_query($dbh, 'SELECT * FROM PRICE');
	$s .= "\t<PRICE>\n";
	$rw = 0;
	while ($row = ibase_fetch_object($h))
	{
		$s .= "\t\t<ROW>\n";
		$s .= "\t\t\t<HID>$row->HID</HID>\n";
		$s .= "\t\t\t<ACTION_NAME>$row->ACTION_NAME</ACTION_NAME>\n";
		$s .= "\t\t\t<PRICE>$row->PRICE</PRICE>\n";
		$s .= "\t\t\t<ID>$row->ID</ID>\n";
		$s .= "\t\t</ROW>\n";
	};
	$s .= "\t</PRICE>\n";
	ibase_free_result($h);

	$h = ibase_query($dbh, 'SELECT * FROM PRICE_LISTS');
	$s .= "\t<PRICE_LISTS>\n";
	$rw = 0;
	while ($row = ibase_fetch_object($h))
	{
		$s .= "\t\t<ROW>\n";
		$s .= "\t\t\t<GID>$row->GID</GID>\n";
		$s .= "\t\t\t<HID>$row->HID</HID>\n";
		$s .= "\t\t\t<ACTION_NAME>$row->ACTION_NAME</ACTION_NAME>\n";
		$s .= "\t\t\t<PRICE>$row->PRICE</PRICE>\n";
		$s .= "\t\t\t<ID>$row->ID</ID>\n";
		$s .= "\t\t</ROW>\n";
	};
	$s .= "\t</PRICE_LISTS>\n";
	ibase_free_result($h);

	$h = ibase_query($dbh, 'SELECT * FROM PRICEGROUPS');
	$s .= "\t<PRICEGROUPS>\n";
	$rw = 0;
	while ($row = ibase_fetch_object($h))
	{
		$s .= "\t\t<ROW>\n";
		$s .= "\t\t\t<ID>$row->ID</ID>\n";
		$s .= "\t\t\t<COMMENT>$row->COMMENT</COMMENT>\n";
		$s .= "\t\t</ROW>\n";
	};
	$s .= "\t</PRICEGROUPS>\n";
	ibase_free_result($h);

	$h = ibase_query($dbh, 'SELECT * FROM PRICEHEADER');
	$s .= "\t<PRICEHEADER>\n";
	$rw = 0;
	while ($row = ibase_fetch_object($h))
	{
		$s .= "\t\t<ROW>\n";
		$s .= "\t\t\t<HEADER>$row->HEADER</HEADER>\n";
		$s .= "\t\t\t<ID>$row->ID</ID>\n";
		$s .= "\t\t</ROW>\n";
	};
	$s .= "\t</PRICEHEADER>\n";
	ibase_free_result($h);

	$s .= "</database>\n";

	echo $s;

	ibase_close($dbh);


?>