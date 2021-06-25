create view mess_tags(id, tag) as
SELECT mess.id,
       unnest.unnest AS tag
FROM mess.mess,
     LATERAL unnest(string_to_array((xpath('//@tags'::text, mess.content))[1]::text, ','::text)) unnest(unnest);

alter table mess_tags
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_tags to yavulan;

