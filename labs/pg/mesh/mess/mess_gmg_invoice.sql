create view mess_gmg_invoice(date, tasks, hours, unit, price, costs) as
SELECT to_char(mess_gmg_done_days.date::timestamp with time zone, 'dd.MM.yyyy'::text) AS date,
       mess_gmg_done_days.tasks,
       mess_gmg_done_days.hours::numeric(6, 2)                                        AS hours,
       'hour/час'::text                                                               AS unit,
       '€40.00'::text                                                                 AS price,
       '€'::text || (mess_gmg_done_days.hours * 40::numeric)::numeric(6, 2)::text     AS costs
FROM mesh.mess_gmg_done_days
ORDER BY mess_gmg_done_days.date;

alter table mess_gmg_invoice
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_gmg_invoice to yavulan;

