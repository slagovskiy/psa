using System;
using System.Collections.Generic;

namespace PSA.Lib.Util
{
	public enum Modules
	{
		Acceptance = 1,
		Administrator = 2,
		Designer = 3,
		Operator = 4,
		Exchanger = 5,
		Robot = 6,
		Inventory = 7,
		Kiosk = 8
	};

    public enum OrderActions
    {
        none = 0,
        PrintCheck = 1,
        MovePrint = 2,
        MoveEdit = 3,
        MovePreview = 4,
        MoveWaitPay = 5,
        MoveToAward = 6,
		MovePayment= 7
    }

}