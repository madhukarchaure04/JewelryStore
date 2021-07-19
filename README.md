# JewelryStore
### Jewelry store REST APIs

#### Prerequisites:
* Visual studio 2019
* .Net Core 3.1

#### Steps to run the application locally:
* Clone the repository to local machine
* Restore all the dependencies
* Run the project

#### Postman test suites:
1. [JewelryStore API Privileged user workflow](https://www.getpostman.com/collections/9360fba00d3e10c344f8)
2. [JewelryStore API Regular user workflow](https://www.getpostman.com/collections/4e912e3651fe9238bc33)
##### Note: Postman collection has configured to use the variable **localUrl** which is default set to **https://localhost:44384**. If your application is running at another port or another server, please modify thi variable accordingly.

#### Known Issue:
* This application uses DinkToPDF library for creating PDF file. This application requires the library [libwkhtmltox.dll](https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dll) to be manually downloaded and kept at **"C:\Program Files\IIS Express"** path.

#### The application will run on port 44384 by default and swagger is configured at '/swagger' path.
