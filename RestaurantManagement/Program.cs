using RestaurantManagement.BusinessLogic;
using RestaurantManagement.Presentation;
using System;
using System.Text;


Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
var menuManager = new MenuManager();
var waiterManager = new WaiterManager();
var receiptManager = new ReceiptManager(menuManager, waiterManager);

var menu = new ConsoleMenu(menuManager, receiptManager, waiterManager);
menu.Run();