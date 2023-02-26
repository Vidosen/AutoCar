SELECT DISTINCT contract."CarNumber"
FROM "Contracts" AS contract LEFT JOIN "Cars" car ON car."Number" = contract."CarNumber"
WHERE car."Brand" = @Brand
