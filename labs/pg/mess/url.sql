create table mess.url (
    id serial not null,
    constraint pk_mess_url primary key (id),

    url ltree not null,
    constraint ux_mess_url_url unique (url)
);
