A comprehensive desktop application developed in **C# (Windows Forms)** designed to help users manage their personal library and track reading habits. This application utilizes **SQLite** for robust local data storage and offers features like reading statistics and cover image management.

Features

* Book Management:** Easily Add, Update, and Delete books from your digital library.
* Reading Statistics:** Automatically calculates the **Average Pages Read per Day** based on the start and end dates entered.
* Cover Image Support:** Upload and display cover images for each book directly within the application grid.
* Live Search:** Real-time filtering capability to find books instantly by name.
* SQLite Database:** Uses a local `Kitaplar.sqlite` database for persistent and reliable data storage.
* Data Integrity:** Includes tools to recalculate statistics for the entire library to ensure data accuracy.

Technologies Used

* Language:** C#
* Framework:** .NET Framework (Windows Forms)
* Database:** SQLite (`System.Data.SQLite`)
* IDE:** Visual Studio

Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/Efelgz/C-Sharp_Library_Program]
    ```
2.  **Open the project:**
    Open the solution file (`.sln`) in Visual Studio.
3.  **Restore NuGet Packages:**
    Ensure `System.Data.SQLite` is installed. If not, run the following command in the Package Manager Console:
    ```powershell
    Install-Package System.Data.SQLite
    ```
4.  **Run the Application:**
    Press `F5` or click "Start". The database file (`Kitaplar.sqlite`) will be created automatically upon the first launch.
