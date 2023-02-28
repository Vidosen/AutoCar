CREATE DATABASE autocar_db
    WITH
    OWNER = postgres
    ENCODING = 'UTF8';

CREATE TABLE "Clients"
(
    "Id" SERIAL PRIMARY KEY,
    "FirstName" VARCHAR(100) NOT NULL,
    "LastName" VARCHAR(100) NOT NULL,
    "Patronymic" VARCHAR(100),
    "BirthDate" DATE NOT NULL,
    "PhoneNumber" VARCHAR(10)
);

CREATE TABLE "Cars"
(
    "Number" VARCHAR(9) PRIMARY KEY,
    "Brand" VARCHAR(100),
    "ReleaseYear" SMALLINT NOT NULL
);

CREATE TABLE "ParkingSeats"
(
    "Id" SMALLINT PRIMARY KEY,
    "Price" MONEY NOT NULL,
    "ContractId" INTEGER,
    FOREIGN KEY ("ContractId")
        REFERENCES "Contracts" ("Id")
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE TABLE "Contracts"
(
    "Id" INTEGER PRIMARY KEY,
    "ClientId" INTEGER NOT NULL,
    "CarNumber" VARCHAR(9) NOT NULL,
    "SeatId" SMALLINT,
    "ContractDate" DATE NOT NULL,
    "AccuralDate" DATE,
    "Debt" MONEY NOT NULL,
    "PaymentDate" DATE,
    "PaymentAmount" MONEY,
    FOREIGN KEY ("CarNumber")
        REFERENCES "Cars" ("Number")
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    FOREIGN KEY ("ClientId")
        REFERENCES "Clients" ("Id")
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    FOREIGN KEY ("SeatId")
        REFERENCES "ParkingSeats" ("Id")
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);

CREATE INDEX "IX_Contracts_CarNumber"
    ON "Contracts" USING btree ("CarNumber" ASC);

CREATE INDEX "IX_Contracts_ClientId"
    ON "Contracts" USING btree ("ClientId" ASC);

CREATE UNIQUE INDEX "IX_Contracts_SeatId"
    ON "Contracts" USING btree ("SeatId" ASC);

CREATE UNIQUE INDEX "IX_ParkingSeats_ContractId"
    ON "ParkingSeats" USING btree ("ContractId" ASC);
