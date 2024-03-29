winForm应用程序，开发环境：.net5. vs2019 Preview 版本 16.9.0 Preview 2.0
# GroupImages
把多张图片依据Gps的经纬度信息算出它们之间的距离，并按指定的距离容差进行分组。

举例：比如，我们航拍了6000张照片放在一个文件夹下，现在需要把它们之间相距在200米内的分为同一组，相距200米以上的则划分为另一组。则通过本程序计算后，会把它们按200米的距离容差划分为了可能25个分组，并且为每个分组创建一个文件夹。

有两种方式来读取gps信息：

1.可以从pos文件信息进行读取图片文件的经纬度信息。
要求pos文件为cvs格式或以半角逗号为分隔符每行一条图片信息的txt格式文件，文件列需包含1.图片文件全路径 2.double类型的纬度信息 3.double类型的经度信息

2.直接从指定文件夹来读取所有图片的gps经纬度信息。

一些解释：

【距离容差】（单位：米）
指从两个图片的经纬度信息算出地球弧长，弧长即为两者之间的距。当两者距离小于参数【距离容差】值时，则认为这两者应该归属于同一分组。

【该程序存在的缺陷】
1.该程序在计算球面两点的弧长时，球的半径采用了一个固定的常数 6378137，实际上球半径会依据距离地心的不同的高度而不同，所以，这里并不精确。
2.执行分组的算法采用冒泡法进行递归计算，运算量较大，算法还有很大的性能改进空间。
3.该程序并没有进行大量的生产环境实战。

程序运行界面如下：

![image](https://user-images.githubusercontent.com/46520648/155046808-2c739bc6-ad1e-4bdc-a14e-a5b4d5a903bf.png)

Englishi below:
# GroupImages
WinForm application, development environment: net5. Vs2019 preview version 16.9.0 preview 2.0

The distance between the images is calculated according to the longitude and latitude information of GPS, and grouped according to the specified distance tolerance.

For example, we took 6000 photos aerial and put them in a folder. Now we need to divide them into the same group within 200 meters, and divide them into another group with a distance of more than 200 meters. After calculation by this program, they will be divided into possible 25 groups according to the distance tolerance of 200 meters, and a folder will be created for each group.

There are two ways to read GPS information:
1. The longitude and latitude information of image file can be read from POS file information.
POS file is required to be in CVS format or TXT format with half width comma as separator, and file column should contain 1. Full path of image file 2. Latitude information of double type 3. Longitude information of double type
2. Read the GPS longitude and latitude information of all pictures directly from the specified folder.
Some explanations:
[distance tolerance] (unit: m)
It refers to calculating the earth arc length from the longitude and latitude information of two pictures, and the arc length is the distance between them. When the distance between them is less than the value of the parameter [distance tolerance], they should belong to the same group.

[defects in the procedure]
1. When calculating the arc length of two points on the sphere, the radius of the ball adopts a fixed constant of 6378137. In fact, the radius of the ball varies with the height from the earth's center, so it is not accurate here.
2. Bubble method is used for recursion calculation in the algorithm of executing grouping, which has a large amount of calculation, and there is still a lot of room for performance improvement.
3. The program does not carry out a large number of production environment actual combat.
