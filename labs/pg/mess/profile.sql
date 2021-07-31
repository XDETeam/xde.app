create table mess.profile (
    id serial not null,
    constraint pk_mess_profile primary key (id),

    name text not null,
    constraint ux_mess_profile_name unique (name),

    content xml null,

    notes xml null
);
