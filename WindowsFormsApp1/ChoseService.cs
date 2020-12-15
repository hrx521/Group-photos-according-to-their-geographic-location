using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /*
    照片的经纬度信息，是以度分秒的格式展示的（度分秒之间以“；”间隔），为了方便定位，
    我们需要将度分秒转换为度的格式。如本例纬度为：32;49;49.942932000005413;
    计算方法为：32+(49+49.9429/60)/60=32.8305;同样方法计算出经度为：106.2947；（小数位越多，经度越高）
     */

    public  class GroupInfo
    {
        /// <summary>
        /// 每一组的组名，实际上还是图片完整路径名
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 每一组的组成员列表，实际上就是成员图片的完整路径列表
        /// </summary>
        public List<ImageInfo> MemberList { get; set; }
    }
    public class ImageInfo
    {
        /// <summary>
        /// 图片文件完整路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
    }
    public class ChoseServiceProcessChanged:EventArgs
    {
        private readonly string processText;

        public ChoseServiceProcessChanged(string processText)
        {
            this.processText = processText;
        }
        public string ProcessText { get; private set; }
    }
    public class ChoseService
    {
        private ChoseService()
        {

        }
        public ChoseService(string directorySource, string DirectoryDest, int tolorence, bool isCopy = true)
        {
            this.directorySource = directorySource;
            this.directoryDest = DirectoryDest;
            this.tolorence = tolorence;
            this.IsCopy = isCopy;
        }
        //private List<GroupInfo> groupList = new List<GroupInfo>();
        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;
        private readonly string directorySource;
        private readonly string directoryDest;
        private readonly int tolorence;
        private readonly bool IsCopy = true;
        private string _discription="工作开始。。。";

        public string Discription 
        {
            get { return Discription; }
            set
            {
                _discription = value;
                Console.WriteLine(_discription);
            } 
        }
        public static Action<object,ChoseServiceProcessChanged> UpdateProcess;

        public async Task<ChoseService> ExcuteGroup()
        {
            Discription = "正在读取文件GPS信息。。。";
            List<ImageInfo> imageInfos = await GetImagesInfo(this.directorySource);
            Discription = "正在计算分组。。。";
            List<GroupInfo> groupList = new List<GroupInfo>();
            await Task.Run(()=>GetGroupList(imageInfos, ref groupList));

#region 分组预览
            {
                int g = 1;
                int gcount = groupList.Count();
                Console.WriteLine($"=====分组预览：容差{this.tolorence}米时，共{gcount }组=================================");
                foreach (var groupitem in groupList)
                {
                    Console.WriteLine($"==========================分组{g++}/共{gcount }组=================================");
                    string DestDirectory = @$"{directoryDest}\{groupitem.GroupName.Split('\\').Last().Split('.')[0]}";
                    //if (!Directory.Exists(DestDirectory))
                    //    Directory.CreateDirectory(DestDirectory);
                    int m = 1;
                    int mcount = groupitem.MemberList.Count();
                    foreach (var imageItem in groupitem.MemberList)
                    {
                        Console.Write($"\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b  图片：{m++}/共{mcount }张");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("=====分组预览结束========");
            }
            if (MessageBox.Show("是否要按以上分组预览情况进行分组", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                Console.WriteLine("用户不想按以上分组预览进行分组，停止了程序运行");
                throw new Exception("用户不想按以上分组预览进行分组，停止了程序运行");
            }
# endregion 分组预览
            Discription = "正在写入分组后的文件。。。";
            await Task.Run(() =>
            {
                int g = 1;
                int gcount = groupList.Count();
                foreach (var groupitem in groupList)
                {
                    Console.WriteLine($"==========================当前分组：{g++}/共{gcount }组=================================");
                    string DestDirectory = @$"{directoryDest}\{groupitem.GroupName.Split('\\').Last().Split('.')[0]}";
                    if (!Directory.Exists(DestDirectory))
                        Directory.CreateDirectory(DestDirectory);
                    int m = 1;
                    int mcount = groupitem.MemberList.Count();
                    foreach (var imageItem in groupitem.MemberList)
                    {
                        Console.Write($"\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b当前图片：{m++}/共{mcount }张");
                    ReTry:
                        if (IsCopy)
                            try
                            {
                                File.Copy(imageItem.Path, DestDirectory + @"\" + imageItem.Path.Split('\\').Last(), true);
                            }
                            catch(Exception ex)
                            {
                                if (MessageBox.Show(ex.Message, "异常", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                                    goto ReTry;
                                else
                                {
                                    Console.WriteLine("用户选择停止了程序运行");
                                    throw new Exception("用户选择停止了程序运行");
                                }

                            }
                        else
                        {
                            try
                            {
                                File.Move(imageItem.Path, DestDirectory + @"\" + imageItem.Path.Split('\\').Last(), true);
                            }
                            catch (Exception ex)
                            {
                                if (MessageBox.Show(ex.Message, "异常", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                                    goto ReTry;
                                else
                                {
                                    Console.WriteLine("用户选择停止了程序运行");
                                    throw new Exception("用户选择停止了程序运行");
                                }
                            }
                        }
                            
                    }
                    Console.WriteLine("");
                }
            }
            );
             Console.WriteLine("==========所有任务都已经完成！");
            return this;
        }

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        private static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }



        /// <summary>
        /// 从jpg中读取经纬度、拍摄时间、修改时间：
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private ImageInfo GetLocationFromPic(String imagePath)
        {
            //List<string> sXY = new List<string>();
            ImageInfo imageInfo = new ImageInfo();
            imageInfo.Path = imagePath;
            try
            {
                //载入图片   
                Image objImage = Image.FromFile(imagePath);
                //取得所有的属性(以PropertyId做排序)   
                var propertyItems = objImage.PropertyItems.OrderBy(x => x.Id);
                foreach (PropertyItem objItem in propertyItems)
                {
                    //只取Id范围为0x0000到0x001e
                    if (objItem.Id >= 0x0000 && objItem.Id <= 0x001e)
                    {
                        switch (objItem.Id)
                        {
                            case 0x0002://设置纬度
                                if (objItem.Value.Length == 24)
                                {
                                    //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint)   
                                    double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                    //minutes(將byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)   
                                    double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                    //seconds(將byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)   
                                    double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                    //double dblGPSLatitude = (((s / 60 + m) / 60) + d);
                                    //sXY.Add(dblGPSLatitude.ToString("0.00000000"));
                                    imageInfo.Latitude= (((s / 60 + m) / 60) + d);
                                }
                                break;
                            case 0x0004: //设置经度
                                if (objItem.Value.Length == 24)
                                {
                                    //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint)   
                                    double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                    //minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)   
                                    double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                    //seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)   
                                    double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                    //double dblGPSLongitude = (((s / 60 + m) / 60) + d);
                                    //sXY.Add(dblGPSLongitude.ToString("0.00000000"));
                                    imageInfo.Longitude = (((s / 60 + m) / 60) + d);
                                }
                                break;
                        }
                    }
                    //if (objItem.Id == 0x9003 || objItem.Id == 0x0132)//Id为0x9003表示拍照的时间,0x0132 最后更新时间
                    //{
                    //    var propItemValue = objItem.Value;
                    //    var dateTimeStr = System.Text.Encoding.ASCII.GetString(propItemValue).Trim('\0');
                    //    var dt = DateTime.ParseExact(dateTimeStr, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
                    //    sXY.Add(dt.ToString());//.ToShortDateString()
                    //}
                }

                //objImage.Dispose();
                return imageInfo;

            }
            catch (Exception ex)
            {
                MessageBox.Show(imagePath + "  该图片文件无法读取GPS信息,异常信息："+ex.Message);
                //listErrorMessage.Add(jpgPath + "该照片由于照片损坏，因此无法进行导入。");
                throw;
            }

        }


        private async Task< List<ImageInfo>> GetImagesInfo(string sourcePath,bool sourceDataFromPosfileOrImages=true)
        {
            List<ImageInfo> list = new List<ImageInfo>();
            if (sourceDataFromPosfileOrImages)
            {
                List<string[]> posList = GroupImages.CsvHelper.ReadCSV(sourcePath);
                if (posList.Count == 0)
                    throw new ArgumentNullException(nameof(posList));
                if (!posList[0][0].EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                    posList.RemoveAt(0);
                int count = posList.Count();
                int i = 1;
                foreach (var item in posList)
                {
                    Console.Write($"\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b{i++}/{count }");
                    ImageInfo imageInfo = new ImageInfo() { Path = item[0].Replace('/','\\'), Latitude = Convert.ToDouble(item[1]), Longitude = Convert.ToDouble(item[2]) };
                    await Task.Run(() => list.Add(imageInfo));
                }
            }
            else
            {
                IEnumerable<string> files = Directory.GetFiles(sourcePath).Where(s => s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase));
                int count = files.Count();
                int i = 1;
                foreach (var item in files)
                {
                    Console.Write($"\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b{i++}/{count }");
                    await Task.Run(() => list.Add(GetLocationFromPic(item)));
                }
            }
            Console.WriteLine("");
            Console.WriteLine("================所有文件GPS信息读取完毕=======================");
            return list;
        }
        /// <summary>
        /// 算法：
        /// 1.先取第一张图片做为组名，然后遍历所有图片当距离小于指定常量时，认为就是这一组的成员则把它放进这一组的成员列表中。
        /// 2.生成新组后，再遍历这一组的成员与剩余没有归属到组中的成员进行距离逐个比较，当这个距离还小于指定常量时，再把它也入到组中。
        /// 3.如此，把剩余没有归属的成员列表与组信息做为变量再进行前面的遍历。直到所有成员都被分组。
        /// </summary>
        /// <param name="imageInfos"></param>
        /// <param name="groups"></param>
        public void GetGroupList( List<ImageInfo> imageInfos, ref List<GroupInfo> groups)
        {
            if (imageInfos is null || imageInfos.Count == 0)
                return;
            List<ImageInfo> restImageInfos = new List<ImageInfo>();
            imageInfos.ForEach(c=>restImageInfos.Add(c));

            GroupInfo group = new GroupInfo();
            ImageInfo topImageInfo = imageInfos[0];
            group.GroupName = topImageInfo.Path;
            group.Longitude = topImageInfo.Longitude;
            group.Latitude = topImageInfo.Latitude;
            group.MemberList = new List<ImageInfo>();
            groups.Add(group);
            restImageInfos.Remove(restImageInfos.FirstOrDefault(c => c.Path == topImageInfo.Path));

            foreach (var item in imageInfos)
            {
                if (GetDistance(group.Latitude, group.Longitude, item.Latitude, item.Longitude)<= tolorence)
                {
                    group.MemberList.Add(item);
                    restImageInfos.Remove(restImageInfos.FirstOrDefault(c=>c.Path==item.Path));
                }
            }
            for (int k= group.MemberList.Count-1;k>=0;k--)
            {
                for (int i = restImageInfos.Count - 1; i >= 0; i--)
                {
                    if (GetDistance(group.Latitude, group.Longitude, restImageInfos[i].Latitude, restImageInfos[i].Longitude) <= tolorence)
                    {
                        group.MemberList.Add(restImageInfos[i]);
                        restImageInfos.Remove(restImageInfos[i]);
                    }
                }
            }
            GetGroupList(restImageInfos,ref groups);

        }



        #region 获取图片中的GPS坐标点的算法2
        /// <summary>
        /// 获取图片中的GPS坐标点
        /// </summary>
        /// <param name="jpgPath">图片路径</param>
        /// <returns>返回坐标【纬度+经度】用"+"分割 取数组中第0和1个位bai置的值</returns>
        private String fnGPSLocation(String jpgPath)
        {
            String s_GPSLocation = "";
            //载入图片
            Image objImage = Image.FromFile(jpgPath);
            //取得所有的属性(以PropertyId做排序)
            var propertyItems = objImage.PropertyItems.OrderBy(x => x.Id);
            //暂定纬度N(北纬)
            char chrGPSLatitudeRef = 'N';
            //暂定经度为E(东经)
            char chrGPSLongitudeRef = 'E';
            foreach (PropertyItem objItem in propertyItems)
            {
                //只取Id范围为0x0000到0x001e
                if (objItem.Id >= 0x0000 && objItem.Id <= 0x001e)
                {
                    objItem.Id = 0x0002;
                    switch (objItem.Id)
                    {
                        case 0x0000:
                            var query = from tmpb in objItem.Value select tmpb.ToString();
                            string sreVersion = string.Join(".", query.ToArray());
                            break;
                        case 0x0001:
                            chrGPSLatitudeRef = BitConverter.ToChar(objItem.Value, 0);
                            break;
                        case 0x0002:
                            if (objItem.Value.Length == 24)
                            {
                                //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint)
                                double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                //minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)
                                double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                //seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)
                                double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                //计算经纬度数值, 如果是南纬, 要乘上(-1)
                                double dblGPSLatitude = (((s / 60 + m) / 60) + d) * (chrGPSLatitudeRef.Equals('N') ? 1 : -1);
                                string strLatitude = string.Format("{0:#} deg {1:#}' {2:#.00}\" {3}", d
                                , m, s, chrGPSLatitudeRef);
                                //纬度+经度
                                s_GPSLocation += dblGPSLatitude + "+";
                            }
                            break;
                        case 0x0003:
                            //透过BitConverter, 将Value转成Char('E' / 'W')
                            //此值在后续的Longitude计算上会用到
                            chrGPSLongitudeRef = BitConverter.ToChar(objItem.Value, 0);
                            break;
                        case 0x0004:
                            if (objItem.Value.Length == 24)
                            {
                                //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint)
                                double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                //minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)
                                double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                //seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)
                                double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                //计算精度的数值, 如果是西经, 要乘上(-1)
                                double dblGPSLongitude = (((s / 60 + m) / 60) + d) * (chrGPSLongitudeRef.Equals('E') ? 1 : -1);
                            }
                            break;
                        case 0x0005:
                            string strAltitude = BitConverter.ToBoolean(objItem.Value, 0) ? "0" : "1";
                            break;
                        case 0x0006:
                            if (objItem.Value.Length == 8)
                            {
                                //将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint
                                double dblAltitude = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                            }
                            break;
                    }
                }
            }
            return s_GPSLocation;
        }
        #endregion

    }

}
