//! Specs are responsible for describing meta-information.

use std::{ vec::Vec };

pub trait Schema {
    fn to_schema();
}

pub struct SchemaProperty {
    pub name: String,
    pub kind: String,
}

pub struct SchemaNode {
    pub name: String,
    pub props: Vec<SchemaProperty>,
}
