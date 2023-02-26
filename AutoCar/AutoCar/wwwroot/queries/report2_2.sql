SELECT DISTINCT client."Id",
        CONCAT(client."LastName", ' ', client."FirstName", ' ', client."Patronymic") AS "FullName"
FROM "Contracts" AS contract
JOIN "Clients" AS client ON contract."ClientId" = client."Id"
WHERE contract."CarNumber" = @CarNumber;