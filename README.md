# Appplicant Database Console Project
My first task, building a C# console application. This application will gather data from the database, and return organized data in the form of a summary report.

## Tools
- Visual Studio
- Git
- SSMS

## Instructions to Run
To run the console app, open the folder in Visual Studio. Press the run button, and the report will generate.
## Database
The project uses SQL for the database, through SSMS

## Mockaroo 
Mockaroo was used to generate sample data to populate the table, allowing for me to test database functions as if the database were populated with real information. 

## Entity Framework setup summary
Though PMC, I was able to get the entity framerwork going. After removing failed attempts or false leads, I ran the scaffolding command which holds similar information to that found in an appsettings.json file.
## Connection string/security notes
By using Windows authentication, sensitive information like the user id or the password of a specific instance to a database were hidden.
## Example report output
```
Applicant Summary Report
------------------------

Total Applicants: 1000

Applicants by State
:       825
01:     3
03:     10
04:     2
05:     2
06:     5
08:     2
10:     1
1084:   1
11:     3
12:     1
13:     4
15:     2
16:     3
17:     1
18:     3
19:     1
47:     1
97:     1
99:     1
A3:     1
A7:     3
A8:     12
A9:     1
AB:     5
AL:     2
AN:     2
B1:     2
B2:     1
B3:     3
B5:     3
B6:     2
B8:     1
B9:     5
BC:     2
BCN:    1
BD:     1
BU:     1
CA:     1
CHP:    3
COL:    1
DC:     1
DE:     1
E:      2
ENG:    2
F:      4
FL:     2
GA:     1
GF:     1
GRO:    1
GUA:    2
H:      1
IA:     1
JAL:    1
JHR:    1
KS:     1
LG:     1
M:      1
MB:     2
MD:     1
MEX:    1
MLK:    1
MN:     1
MOR:    1
N:      1
NC:     3
NS:     2
NW:     2
NY:     1
O:      3
OAX:    1
OH:     1
PNG:    1
QC:     3
SIN:    1
SK:     1
SRW:    1
T:      1
TAM:    1
TAS:    1
TN:     3
TX:     4
U:      1
VA:     2
VER:    3
WA:     1
WV:     2
X:      2
Y:      1
Z:      1

Applicants by Household Size
Small(1-3): 275
Medium(4-6): 297
Large(7+): 428

Applicants with Children: 916

Applicants with Food insecuirty/assistance need indicators: 761

Mandy Daen      | mdaen0@livejournal.com        | pending

Raff Parradice  | rparradice1@1und1.de          | denied

Anderson Mont   | amont2@yellowpages.com        | pending

Zack Loddy      | zloddy3@vistaprint.com        | pending

Sheelah Lettuce | slettuce4@mit.edu             | pending

Joly Alliberton | jalliberton5@nydailynews.com  | denied

Worthy Grote    | wgrote6@icq.com               | approved

Sella Caustic   | scaustic7@harvard.edu         | approved

Orly Cuttle     | ocuttle8@technorati.com       | approved

Amara Kores     | akores9@opera.com             | pending

```
## Known issues or setup challenges
It was a challenge to incorporate EF into the project for me since it was my first time using it. There was a lot of torubleshooting involved and a lot of chats with AI trying to figure out how to fix the issues before I could actually sit down and get into the coding.
