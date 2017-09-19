using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XCommerce.Bot
{
    public class BotConnection
    {
       
        public DirectLineClient Client;
        public Conversation MainConversation;
        public ChannelAccount Account;

        public BotConnection(string accountName)
        {
            Client = new DirectLineClient(ConstantHelper.DIRECTLINE_KEY);
            MainConversation = Client.Conversations.StartConversation();
            Account = new ChannelAccount { Id = accountName, Name = accountName };
        }

        public void SendMessage(string message)
        {
            Activity activity = new Activity
            {
                From = Account,
                Text = message,
                Type = ActivityTypes.Message
            };
            Client.Conversations.PostActivity(MainConversation.ConversationId, activity);
        }

        public async Task GetMessagesAsync(ObservableCollection<MessageListItem> collection)
        {
            string watermark = null;
            //Loop retrieval
            while (true)
            {

                // Debug.WriteLine("Reading message every 3 seconds");
                //Get activities (messages) after the watermark
                var activitySet = await Client.Conversations.GetActivitiesAsync(MainConversation.ConversationId,
                watermark);
                //Set new watermark
                watermark = activitySet?.Watermark;
                //Loop through the activities and add them to the list
                foreach (Activity activity in activitySet.Activities)
                {


                    if (activity.From.Name == ConstantHelper.BOT_NAME)
                    {
                        if (activity.Attachments != null && activity.Attachments.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            var attachment = activity.Attachments[0];
                            var content = attachment.Content;

                            Dictionary<string, object> jsonContent = JsonConvert.DeserializeObject<Dictionary<string, object>>(content.ToString());

                            foreach (var item in jsonContent)
                            {
                                if (item.Key == "text")
                                {
                                    sb.AppendLine(item.Value.ToString() + " : ");
                                }
                                else if (item.Key == "buttons")
                                {
                                    List<Button> buttons = JsonConvert.DeserializeObject<List<Button>>(item.Value.ToString());
                                    foreach (var butt in buttons)
                                    {
                                        sb.AppendLine(butt.value);
                                    }
                                }
                            }

                            collection.Add(new MessageListItem(sb.ToString(), activity.From.Name));
                            //listView.ScrollTo((listView.BindingContext as List<MessageListItem>)[0] , ScrollToPosition.MakeVisible, true);
                        }
                        else
                        {
                            collection.Add(new MessageListItem(activity.Text, activity.From.Name));
                        }
//                        Debug.WriteLine(activity.From.Name);

 //                       Debug.WriteLine(activity.Text);

                        
                    }
                }
                //Wait 3 seconds
                await Task.Delay(3000);
            }
        }
    }
}
