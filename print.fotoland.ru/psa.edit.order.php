<?php
include("config.inc");
$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

if($_GET["action"] != "")
{
	switch($_GET["action"])
	{
		case "find_term":
		{
			$s = <<<EOD
				<html>
					<head>
						<title>Редактирование заказа</title>
					</head>
					<body>
						<h1>Редактирование заказа с фототерминала</h1>
						<FORM METHOD="GET" ACTION="">
						<table border=0>
							<tr>
								<td>Местоположение</td>
EOD;
								$s .= "<TD><SELECT NAME=\"P\">";
								$query = <<<EOD
									SELECT 
										KIOSKS.ID,
										KIOSK_GROUP.GROUP_NAME || ' - ' || KIOSKS.ADDRESS AS NAME
									FROM
										KIOSK_GROUP
										INNER JOIN KIOSKS ON (KIOSK_GROUP.ID = KIOSKS.GROUPID)
									ORDER BY
										KIOSKS.GROUPID
EOD;
								$h = ibase_query($dbh, $query);
								while ($row = ibase_fetch_object($h))
								{
									$s .= "<OPTION VALUE=\"$row->ID\">$row->NAME ($row->ID)</OPTION>";
								}
								$s .= "</SELECT></TD>";
			$s .= <<<EOD
							</tr>
							<tr>
								<td>Номер</td>
								<td><INPUT TYPE="TEXT" NAME="N"></td>
							</tr>
							<tr>
								<td colspan=2><INPUT TYPE="SUBMIT" NAME="baction" VALUE="Найти"><INPUT TYPE="HIDDEN" NAME="action" VALUE="show_term"></td>
							</tr>
						</table>
						</FORM>
					</body>
				</html>
EOD;
			echo $s;
			break;
		}
		case "show_term":
		{
			$s = <<<EOD
				<html>
					<head>
						<title>Редактирование заказа</title>
					</head>
					<body>
						<h1>Редактирование заказа с фототерминала</h1>
						<FORM METHOD="GET" ACTION="">
						<table border=0>
EOD;
								$s .= "";
								$query = "" .
									"SELECT " .
									"  KIOSK_ORDERS.KID," .
									"  KIOSK_ORDERS.SESSIONID," .
									"  KIOSK_ORDERS.CUSTOMER," .
									"  KIOSK_ORDERS.PHONE," .
									"  KIOSK_ORDERS.TOPRINT_AUTHID," .
									"  KIOSK_ORDERS.TOPRINT_AUTHDATE," .
									"  KIOSK_ORDERS.PRINTED_AUTHID," .
									"  KIOSK_ORDERS.PRINTED_AUTHDATE," .
									"  KIOSK_ORDERS.SHIP_AUTHID," .
									"  KIOSK_ORDERS.SHIP_AUTHDATE," .
									"  KIOSK_ORDERS.STATUS" .
									" FROM " .
									"  KIOSK_ORDERS " .
									" WHERE " .
									"  KIOSK_ORDERS.SESSIONID = " . $_GET["P"] . " " .
									" AND " .
									"  KIOSK_ORDERS.KID = " . $_GET["N"] . ";";
								$h = ibase_query($dbh, $query);
								while ($row = ibase_fetch_object($h))
								{
									$s .= "<tr><td>Номер</td><td>$row->KID</td>";
									$s .= "<tr><td>В печать</td><td><SELECT NAME=\"PU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->TOPRINT_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"EPU\">Изменить</td></tr>";
									$s .= "<tr><td>Отпечатал</td><td><SELECT NAME=\"RU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->PRINTED_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"ERU\">Изменить</td></tr>";
									$s .= "<tr><td>Отправил</td><td><SELECT NAME=\"SU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->SHIP_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"ESU\">Изменить</td></tr>";
									$s .= "<tr><td>Статус</td><td><SELECT NAME=\"ST\"><OPTION>???</OPTION>";
									foreach(split(",", "Идет передача,Принят,В печати,Ручная печать,Отпечатан,Готов,Отменен") as $status)
									{
										$s .= "<OPTION VALUE=\"$status\"";
										if($row->STATUS == $status)
											$s .= " SELECTED ";
										$s .= ">$status</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"EST\">Изменить</td></tr>";

									$s .= "<tr><td colspan=\"3\"><INPUT TYPE=\"SUBMIT\" NAME=\"baction\" VALUE=\"Сохранить\"><INPUT TYPE=\"HIDDEN\" VALUE=\"save_term\" NAME=\"action\"><INPUT TYPE=\"HIDDEN\" VALUE=\"" . $_GET["P"] . "\" NAME=\"P\"><INPUT TYPE=\"HIDDEN\" VALUE=\"" . $_GET["N"] . "\" NAME=\"N\"></td></tr>";
								}
			$s .= <<<EOD
						</table>
						</FORM>
					</body>
				</html>
EOD;
			echo $s;
			break;
		}
		case "save_term":
		{
			$query = "";
			$query .= "UPDATE KIOSK_ORDERS SET ";
			$query .= "  KIOSK_ORDERS.KID = " . $_GET["N"];
			$query .= ", KIOSK_ORDERS.SESSIONID = " . $_GET["P"];
			if($_GET["EPU"] == "on")
			{
				$query .= ", KIOSK_ORDERS.TOPRINT_AUTHID = " . $_GET["PU"];
				$query .= ", KIOSK_ORDERS.TOPRINT_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["ERU"] == "on")
			{
				$query .= ", KIOSK_ORDERS.PRINTED_AUTHID = " . $_GET["RU"];
				$query .= ", KIOSK_ORDERS.PRINTED_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["ESU"] == "on")
			{
				$query .= ", KIOSK_ORDERS.SHIP_AUTHID = " . $_GET["SU"];
				$query .= ", KIOSK_ORDERS.SHIP_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["EST"] == "on")
			{
				$query .= ", KIOSK_ORDERS.STATUS = '" . $_GET["ST"] . "'";
			}
			$query .= " WHERE " .
			"  KIOSK_ORDERS.SESSIONID = " . $_GET["P"] . " " .
			" AND " .
			"  KIOSK_ORDERS.KID = " . $_GET["N"] . ";";
			//echo $query;
			$ok=ibase_query($dbh, $query);
			header('Location: psa.edit.order.php');
			break;
		}
		case "find_opt":
		{
			$s = <<<EOD
				<html>
					<head>
						<title>Редактирование заказа</title>
					</head>
					<body>
						<h1>Редактирование заказа оптовика</h1>
						<FORM METHOD="GET" ACTION="">
						<table border=0>
							<tr>
								<td>Номер</td>
								<td><INPUT TYPE="TEXT" NAME="N"></td>
							</tr>
							<tr>
								<td colspan=2><INPUT TYPE="SUBMIT" NAME="baction" VALUE="Найти"><INPUT TYPE="HIDDEN" NAME="action" VALUE="show_opt"></td>
							</tr>
						</table>
						</FORM>
					</body>
				</html>
EOD;
			echo $s;
			break;
		}
		case "show_opt":
		{
			$s = <<<EOD
				<html>
					<head>
						<title>Редактирование заказа</title>
					</head>
					<body>
						<h1>Редактирование заказа оптовика</h1>
						<FORM METHOD="GET" ACTION="">
						<table border=0>
EOD;
								$s .= "";
								$query = "" .
									"SELECT " .
									"  OPT_ORDERS.ID," .
									"  OPT_ORDERS.TOPRINT_AUTHID," .
									"  OPT_ORDERS.TOPRINT_AUTHDATE," .
									"  OPT_ORDERS.PRINTED_AUTHID," .
									"  OPT_ORDERS.PRINTED_AUTHDATE," .
									"  OPT_ORDERS.SHIP_AUTHID," .
									"  OPT_ORDERS.SHIP_AUTHDATE," .
									"  OPT_ORDERS.STATUS" .
									" FROM " .
									"  OPT_ORDERS " .
									" WHERE " .
									"  OPT_ORDERS.ID = " . $_GET["N"] . ";";
								$h = ibase_query($dbh, $query);
								while ($row = ibase_fetch_object($h))
								{
									$s .= "<tr><td>Номер</td><td>$row->ID</td>";
									$s .= "<tr><td>В печать</td><td><SELECT NAME=\"PU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->TOPRINT_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"EPU\">Изменить</td></tr>";
									$s .= "<tr><td>Отпечатал</td><td><SELECT NAME=\"RU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->PRINTED_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"ERU\">Изменить</td></tr>";
									$s .= "<tr><td>Отправил</td><td><SELECT NAME=\"SU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->SHIP_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"ESU\">Изменить</td></tr>";
									$s .= "<tr><td>Статус</td><td><SELECT NAME=\"ST\"><OPTION>???</OPTION>";
									foreach(split(",", "Идет передача,Принят,В печати,Ручная печать,Отпечатан,Готов,Отменен") as $status)
									{
										$s .= "<OPTION VALUE=\"$status\"";
										if($row->STATUS == $status)
											$s .= " SELECTED ";
										$s .= ">$status</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"EST\">Изменить</td></tr>";

									$s .= "<tr><td colspan=\"3\"><INPUT TYPE=\"SUBMIT\" NAME=\"baction\" VALUE=\"Сохранить\"><INPUT TYPE=\"HIDDEN\" VALUE=\"save_opt\" NAME=\"action\"><INPUT TYPE=\"HIDDEN\" VALUE=\"" . $_GET["P"] . "\" NAME=\"P\"><INPUT TYPE=\"HIDDEN\" VALUE=\"" . $_GET["N"] . "\" NAME=\"N\"></td></tr>";
								}
			$s .= <<<EOD
						</table>
						</FORM>
					</body>
				</html>
EOD;
			echo $s;
			break;
		}
		case "save_opt":
		{
			$query = "";
			$query .= "UPDATE OPT_ORDERS SET ";
			$query .= "  OPT_ORDERS.ID = " . $_GET["N"];
			if($_GET["EPU"] == "on")
			{
				$query .= ", OPT_ORDERS.TOPRINT_AUTHID = " . $_GET["PU"];
				$query .= ", OPT_ORDERS.TOPRINT_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["ERU"] == "on")
			{
				$query .= ", OPT_ORDERS.PRINTED_AUTHID = " . $_GET["RU"];
				$query .= ", OPT_ORDERS.PRINTED_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["ESU"] == "on")
			{
				$query .= ", OPT_ORDERS.SHIP_AUTHID = " . $_GET["SU"];
				$query .= ", OPT_ORDERS.SHIP_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["EST"] == "on")
			{
				$query .= ", OPT_ORDERS.STATUS = '" . $_GET["ST"] . "'";
			}
			$query .= " WHERE " .
			"  OPT_ORDERS.ID = " . $_GET["N"] . " " . ";";
			//echo $query;
			$ok=ibase_query($dbh, $query);
			header('Location: psa.edit.order.php');
			break;
		}
		case "find_inet":
		{
			$s = <<<EOD
				<html>
					<head>
						<title>Редактирование заказа</title>
					</head>
					<body>
						<h1>Редактирование интернет заказа</h1>
						<FORM METHOD="GET" ACTION="">
						<table border=0>
							<tr>
								<td>Номер</td>
								<td><INPUT TYPE="TEXT" NAME="N"></td>
							</tr>
							<tr>
								<td colspan=2><INPUT TYPE="SUBMIT" NAME="baction" VALUE="Найти"><INPUT TYPE="HIDDEN" NAME="action" VALUE="show_inet"></td>
							</tr>
						</table>
						</FORM>
					</body>
				</html>
EOD;
			echo $s;
			break;
		}
		case "show_inet":
		{
			$s = <<<EOD
				<html>
					<head>
						<title>Редактирование заказа</title>
					</head>
					<body>
						<h1>Редактирование интернет заказа</h1>
						<FORM METHOD="GET" ACTION="">
						<table border=0>
EOD;
								$s .= "";
								$query = "" .
									"SELECT " .
									"  ORDERS.ID," .
									"  ORDERS.TOPRINT_AUTHID," .
									"  ORDERS.TOPRINT_AUTHDATE," .
									"  ORDERS.PRINTED_AUTHID," .
									"  ORDERS.PRINTED_AUTHDATE," .
									"  ORDERS.SHIP_AUTHID," .
									"  ORDERS.SHIP_AUTHDATE," .
									"  ORDERS.STATUS" .
									" FROM " .
									"  ORDERS " .
									" WHERE " .
									"  ORDERS.ID = " . $_GET["N"] . ";";
								$h = ibase_query($dbh, $query);
								while ($row = ibase_fetch_object($h))
								{
									$s .= "<tr><td>Номер</td><td>$row->ID</td>";
									$s .= "<tr><td>В печать</td><td><SELECT NAME=\"PU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->TOPRINT_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"EPU\">Изменить</td></tr>";
									$s .= "<tr><td>Отпечатал</td><td><SELECT NAME=\"RU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->PRINTED_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"ERU\">Изменить</td></tr>";
									$s .= "<tr><td>Отправил</td><td><SELECT NAME=\"SU\"><OPTION>???</OPTION>";
									$querya = "SELECT AUTH.ID, AUTH.USERNAME FROM AUTH ORDER BY AUTH.USERNAME";
									$ha = ibase_query($dbh, $querya);
									while ($rowa = ibase_fetch_object($ha))
									{
										$s .= "<OPTION VALUE=\"$rowa->ID\"";
										if($rowa->ID == $row->SHIP_AUTHID)
											$s .= " SELECTED ";
										$s .= ">$rowa->USERNAME ($rowa->ID)</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"ESU\">Изменить</td></tr>";
									$s .= "<tr><td>Статус</td><td><SELECT NAME=\"ST\"><OPTION>???</OPTION>";
									foreach(split(",", "Идет передача,Принят,В печати,Ручная печать,Отпечатан,Готов,Отменен") as $status)
									{
										$s .= "<OPTION VALUE=\"$status\"";
										if($row->STATUS == $status)
											$s .= " SELECTED ";
										$s .= ">$status</OPTION>";
									}
									$s .= "</SELECT></td><td><input type=\"checkbox\" name=\"EST\">Изменить</td></tr>";

									$s .= "<tr><td colspan=\"3\"><INPUT TYPE=\"SUBMIT\" NAME=\"baction\" VALUE=\"Сохранить\"><INPUT TYPE=\"HIDDEN\" VALUE=\"save_inet\" NAME=\"action\"><INPUT TYPE=\"HIDDEN\" VALUE=\"" . $_GET["P"] . "\" NAME=\"P\"><INPUT TYPE=\"HIDDEN\" VALUE=\"" . $_GET["N"] . "\" NAME=\"N\"></td></tr>";
								}
			$s .= <<<EOD
						</table>
						</FORM>
					</body>
				</html>
EOD;
			echo $s;
			break;
		}
		case "save_inet":
		{
			$query = "";
			$query .= "UPDATE ORDERS SET ";
			$query .= "  ORDERS.ID = " . $_GET["N"];
			if($_GET["EPU"] == "on")
			{
				$query .= ", ORDERS.TOPRINT_AUTHID = " . $_GET["PU"];
				$query .= ", ORDERS.TOPRINT_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["ERU"] == "on")
			{
				$query .= ", ORDERS.PRINTED_AUTHID = " . $_GET["RU"];
				$query .= ", ORDERS.PRINTED_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["ESU"] == "on")
			{
				$query .= ", ORDERS.SHIP_AUTHID = " . $_GET["SU"];
				$query .= ", ORDERS.SHIP_AUTHDATE = cast('NOW' as DATE)";
			}
			if($_GET["EST"] == "on")
			{
				$query .= ", ORDERS.STATUS = '" . $_GET["ST"] . "'";
			}
			$query .= " WHERE " .
			"  ORDERS.ID = " . $_GET["N"] . " " . ";";
			//echo $query;
			$ok=ibase_query($dbh, $query);
			header('Location: psa.edit.order.php');
			break;
		}

	}

}
else
{
	$s = <<<EOD
		<html>
			<head>
				<title>Редактирование заказа</title>
			</head>
			<body>
				<h1>Редактирование заказа</h1>
				<ul>
					<li><a href="?action=find_term">Заказ с фототерминала</a></li>
					<li><a href="?action=find_opt">Заказ от оптовика</a></li>
					<li><a href="?action=find_inet">Интренет заказ</a></li>
				</ul>
			</body>
		</html>
EOD;
	echo $s;
}

?>