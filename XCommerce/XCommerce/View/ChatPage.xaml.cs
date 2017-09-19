using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Xamarin.Forms;
using XCommerce.Bot;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace XCommerce
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatPage : ContentPage
    {

        BotConnection connection;
        ObservableCollection<MessageListItem> messageList;


        public ChatPage()
        {
            this.InitializeComponent();
            connection = new BotConnection(ConstantHelper.ACCOUNT_NAME);
            messageList = new ObservableCollection<MessageListItem>();

            MessageListView.ItemsSource = messageList;
            var messageTask = connection.GetMessagesAsync(messageList);
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            var message = ((Entry)sender).Text;
            if (message.Length > 0)
            {
                //Clear entry
                ((Entry)sender).Text = "";
                //Make object to be placed in ListView

                var messageListItem = new MessageListItem(message, connection.Account.Name);
                messageList.Add(messageListItem);
                //Send the message to the bot
                connection.SendMessage(message);
            }
        }
    }
}
