SELECT client."Id", CONCAT(client."LastName", ' ', client."FirstName", ' ', client."Patronymic") AS "FullName",
       (
           SELECT COALESCE(sum(contract."Debt"), 0.0)
           FROM "Contracts" AS contract
           WHERE client."Id" = contract."ClientId"
       ) AS "TotalDebt",
       (
           SELECT contract."PaymentDate"
           FROM "Contracts" AS contract
           WHERE client."Id" = contract."ClientId"
           ORDER BY contract."PaymentDate" DESC
           LIMIT 1
    ) AS "LastPaymentDate"
FROM "Clients" AS client
ORDER BY (SELECT COALESCE(sum(contract."Debt"), 0.0)
    FROM "Contracts" AS contract
    WHERE client."Id" = contract."ClientId")
        DESC
    LIMIT 1;