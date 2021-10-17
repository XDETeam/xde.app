create table mess.spin (
    id serial not null,
    constraint pk_mess_spin primary key (id),

    -- Temporary this is a user. Later can be profile for multi-profile users.
    profile int not null,
    constraint fk_mess_spin_profile foreign key (profile) references mess.profile(id),

    -- Next spin in the chain. If it is null, then it's the last spin for the user.
    spin int null
    constraint fk_mess_spin_chain references mess.spin(id),
    constraint ux_mess_spin_profile_spin unique (profile, spin),

    url int not null,
    constraint fk_mess_spin_url foreign key (url) references mess.url(id),

    content xml null,

    visited timestamp not null
);

create index
    ix_mess_spin_url
on mess.spin (
    url
);

create index
    ix_mess_spin_chain
on mess.spin (
    spin
);
