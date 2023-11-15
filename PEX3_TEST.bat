:: Creates a Variable for the Output File
@SET file="pex_test_results.txt"
@ECHO OFF
:: Erases Everything Currently In the Output File
type NUL>%file%

:: ----------------------------------------
:: TITLE
:: ----------------------------------------
echo PEX TEST CASES (C1C Starkey, C1C Klapp) >> %file%

:: ----------------------------------------
:: BAD EXAMPLES
:: ----------------------------------------
::echo Running Incorrect Test Cases >> %file%
::echo. >> %file%

echo Testing Bad If/While Loops>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\if_while_incorrect.txt >> %file%
echo. >> %file%

echo Testing Bad Declaration/ Assignment Statements>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\declaration_assignment_incorrect.txt >> %file%
echo. >> %file%

echo Testing Bad Constant Assignment>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\constant_assignment_incorrect.txt >> %file%
echo. >> %file%

echo Testing Bad Boolean Operations>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_operation_incorrect.txt >> %file%
echo. >> %file%

echo Testing Bad Arithmetic>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_incorrect.txt >> %file%
echo. >> %file%


:: ----------------------------------------
:: GOOD EXAMPLES
:: ----------------------------------------


echo Testing Good If/While Loops>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\if_while_correct.txt >> %file%
echo. >> %file%

echo Testing Good Declaration Statements>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\declaration_assignment_correct.txt >> %file%
echo. >> %file%

echo Testing Good Constant Assignment>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\constant_assignment_correct.txt >> %file%
echo. >> %file%

echo Testing Good Boolean Operations>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\boolean_operation_correct.txt >> %file%
echo. >> %file%

echo Testing Good Arithmetic>> %file%
bin\Debug\ConsoleApplication.exe testcases\pex3\arithmetic_correct.txt >> %file%
echo. >> %file%


pause