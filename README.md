# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact

#Nik project

HU:

Orvosok (id, név, fokozat, születési idő, pecsétszám, szakterület, rendelő_id);
Betegek (id, név, nem, születési idő, anyja neve, betegség);
Rendelő (id, helye, asszisztens neve, telefonszám, rendelés ideje, rendelést végző cég);
Ellátások (id, orvos_id, beteg_id, kezelés megnevezése, ideje);

EN:

Doctors (id, name, degree, dateofbirth, sealnumber, specialization, clinicsid);
Patients (id, name, gender, dateofbirth, nameofmother, disease);
Clinics (id, phone, address, officehours, name, company);
Treatments (id, doctor_id, patient_id, treatmentdesc, treatmenttime);

HU:

Funkciólista:
Orvosok listázása / hozzáadása / fokozatának módosítása / törlése
Betegek listázása / hozzáadása / betegségének módosítása / törlése
Rendelők listázása / hozzáadása / helyének módosítása / törlése
Ellátások rendelések listázása / hozzáadása / törlése
Rendelők és az ott dolgozó orvosok száma
Páciensek és az utolsó ellátásuk, feltéve, hogy volt valaha ellátásuk
Rendelők betegeinek nemi aránya