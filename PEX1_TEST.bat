:: Creates a Variable for the Output File
@SET file="pex_test_results.txt"
@ECHO OFF
:: Erases Everything Currently In the Output File
type NUL>%file%

:: ----------------------------------------
:: TITLE
:: ----------------------------------------
echo PEX TEST CASES (C1C Starkey) >> %file%

:: ----------------------------------------
:: GOOD EXAMPLES
:: ----------------------------------------
::echo Testing Identifiers >> %file%
::bin\Debug\ConsoleApplication.exe testcases\pex1\test1.txt >> %file%
::echo. >> %file%

echo Testing Strings >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\string_correct.txt >> %file%
echo. >> %file%

echo Testing Integers >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\integer_correct.txt >> %file%
echo. >> %file%

echo Testing Identifiers >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\identifier_correct.txt >> %file%
echo. >> %file%

echo Testing Doubles >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\double_correct.txt >> %file%
echo. >> %file%

echo Testing Symbols >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\symbols_correct.txt >> %file%
echo. >> %file%

echo Testing Comments >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\comment_correct.txt >> %file%
echo. >> %file%
:: ----------------------------------------
:: BAD EXAMPLES
:: ----------------------------------------
::echo Running Incorrect Test Cases >> %file%
::echo. >> %file%

echo Running Inccorect Test Cases >> %file%
echo. >> %file%
echo Testing Comments >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\comment_incorrect1.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\comment_incorrect2.txt >> %file%
echo. >> %file%

echo Testing Strings >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\string_incorrect1.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\string_incorrect2.txt >> %file%
echo. >> %file%

echo Testing Doubles >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\double_incorrect1.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\double_incorrect2.txt >> %file%
echo. >> %file%

echo Testing Integers >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\integer_incorrect1.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\integer_incorrect2.txt >> %file%
echo. >> %file%

echo Testing Symbols >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\symbols_incorrect1.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\symbols_incorrect2.txt >> %file%
echo. >> %file%

echo Testing Identifiers >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\identifier_incorrect1.txt >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex1\identifier_incorrect2.txt >> %file%
echo. >> %file%

pause