@echo off

echo ----------------------------------------------------------------------------
echo IMPORTANT:
echo ----------------------------------------------------------------------------
echo   1.  Create the github repository BEFORE running this batch file.
echo.
echo   2.  Close Unity and Visual Studio BEFORE running this batch file.
echo.
echo   3.  Do NOT add a .gitignore or a README.md file in github:
echo       a.  Add the .gitignore file to you Unity project.
echo       b.  The README.md file is created from this batch file.
echo ----------------------------------------------------------------------------
echo.

rem ----------------------------------------------------------------------------
rem  Configure some git settings.
rem ----------------------------------------------------------------------------
set project_name=3D_Breakout
set editor_version=2022.3.35f1
set local_directory=C:/Repos/Unity/%editor_version%/%project_name%
set remote_origin=https://github.com/RandallPrice-420/%project_name%

rem ----------------------------------------------------------------------------
rem  Display the project information.
rem ----------------------------------------------------------------------------
echo project_name.....:  %project_name%
echo editor_version...:  %editor_version%
echo local_directory..:  %local_directory%
echo remote_origin....:  %remote_origin%
echo.

rem ----------------------------------------------------------------------------
rem  Prompt for the step to perform.
rem ----------------------------------------------------------------------------
set /p step= "Enter the step to perform (F = First time, A = ADD changes and commit, Q = quit):  "
rem echo You entered %step%
if "%step%"=="f" goto First_Time
if "%step%"=="F" goto First_Time
if "%step%"=="a" goto Add_And_Commit
if "%step%"=="A" goto Add_And_Commit
if "%step%"=="q" goto Quit
if "%step%"=="Q" goto Quit

:First_Time
rem ----------------------------------------------------------------------------
rem One-time configuration for this project.
rem
rem  - Configure some github global settings
rem  - Delete the README.md file if it exists
rem  - Create the README.md file
rem  - Initialize the repository
rem  - Add and commit the README.md file
rem  - Refresh the local files from the master branch
rem  - Set the remote origin  
rem  - Push to the remote repository
rem  - Show the status
rem ----------------------------------------------------------------------------
@echo on
git config --global --add safe.directory %local_directory%
git config --global user.email "randall_price@hotmail.com"
git config --global user.name  "Randall Price"
echo.

set filePath=README.md
if exist %filePath% (
    del %filePath%
    echo %filePath% file deleted.
)
echo %project_name%>> %filePath%
echo.>> %filePath%
echo %project_name% game using Unity %editor_version%.>> %filePath%
echo.>> %filePath%

git init
git add %filePath%
git commit -m "Initial project upload."
git branch -M master
git remote add origin %remote_origin%
git push -u origin master
git status
@echo off

echo.
echo First time configuration:
echo   - %filePath% created and commited.
echo.
goto Done

:Add_And_Commit
rem ----------------------------------------------------------------------------
rem  - Prompt for the commit message
rem      Example:  Added Part 1 - Spaceship Controls and Part 2 - Bullets.
rem  - Add and commit the changes
rem  - Push to the remote repository
rem ----------------------------------------------------------------------------
set /p commit_message= "Enter commit message for the ADD (Q to quit):  "
echo You entered %commit_message%
if "%commit_message%"=="q" goto Quit
if "%commit_message%"=="Q" goto Quit

echo on
git pull origin master
git add .
git commit -m "%commit_message%"
git push -u origin master
@echo off

echo.
echo - Changed files committed and pushed to remote repository successfully.
echo.

:Done
:Quit
pause
exit
