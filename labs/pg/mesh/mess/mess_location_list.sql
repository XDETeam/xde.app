create view mess_location_list(location, title, id, content) as
SELECT location.at AS location,
       location.title,
       mess.id,
       mess.content
FROM mess.mess,
     LATERAL XMLTABLE(('//location'::text) PASSING (mess.content)
                      COLUMNS at text PATH ('@at'::text), title text PATH ('../@title'::text)) location
ORDER BY location.at;

alter table mess_location_list
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_location_list to yavulan;

