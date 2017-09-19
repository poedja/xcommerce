
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.IO;
using System.Threading.Tasks;
using System;


using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;

namespace XCommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadPage : ContentPage
    {
        #region Parameters
        string photoUrl = "https://content4.morazzia.com/twistys/3053/10.jpg";
        string photoUrl2 = "https://www.toppers.com/Portals/0/house-pizza-mac-n-cheese.jpg";
        string photoUrl3 = "https://auto.ndtvimg.com/car-images/medium/hyundai/elite-i20/hyundai-elite-i20.jpg";


        VisionServiceClient visionClient;
        MediaFile photo;
        #endregion
        public UploadPage()
        {
            InitializeComponent();
            visionClient = new VisionServiceClient(ConstantHelper.VISION_KEY, (ConstantHelper.VISION_URL));

        }

      

        private async Task<AnalysisResult> CheckImage(Stream stream)
        {
            
            AnalysisResult analysisResult;
            VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };

            analysisResult = await visionClient.AnalyzeImageAsync(stream, visualFeatures);
            if (analysisResult == null)
            {
                throw new Exception("Can't detect image");
            }
            return analysisResult;
        }

        private async Task<AnalysisResult> CheckImage(string photoUrl)
        {

            AnalysisResult analysisResult;
            VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };

            analysisResult = await visionClient.AnalyzeImageAsync(photoUrl, visualFeatures);

            if (analysisResult == null)
            {
                throw new Exception("Can't detect image");
            }
            return analysisResult;
        }

        private bool IsAdult(AnalysisResult result)
        {
            return result.Adult.IsAdultContent;
        }

        private bool IsFood(AnalysisResult result)
        {
            var items = result.Description.Tags;
           
            bool resultFood = false;
            foreach (var item in items)
            {
                if (item.Contains("food"))
                {
                    resultFood = true;
                    return resultFood;
                }
            }
            return resultFood;
        }

        async void OnTakePhotoButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            string photoTaken = photoUrl3;
            // Take photo
            if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
            {
                photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Name = "product.jpg",
                    PhotoSize = PhotoSize.Small
                });

                if (photo != null)
                {
                    //ImageProduct.Source = ImageSource.FromStream(photo.GetStream);
                    ImageProduct.Source = ImageSource.FromUri(new Uri(photoTaken));
                }
            }
            else
            {
                //await DisplayAlert("No Camera", "Camera unavailable.", "OK");
            }

            

            try
            {
                if (photo != null)
                {
                    IndicatorVision.IsVisible = true;
                    var result = await CheckImage(photoTaken);
                    IndicatorVision.IsVisible = false;
                    BoxSensor.IsVisible = false;

                    if (!IsFood(result))
                        await DisplayAlert("Warning!", "You can't upload non-food image", "OK");
                    else
                    {
                    }

                    //if (IsAdult(result))
                    //    await DisplayAlert("Warning!", "Your product contain pornographic content", "OK");
                    //else
                    //{
                    //    //save product to db
                    //}




                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            
        }



    }
}