extern crate proc_macro;

use proc_macro::TokenStream;
use quote::quote;

fn impl_to_schema(ast: &syn::DeriveInput) -> TokenStream {
    let name = &ast.ident;

    let gen = quote! {
        impl Schema for #name {
            fn to_schema() {
                println!("Schema for {}", stringify!(#name));
            }
        }
    };

    gen.into()
}

#[proc_macro_derive(Schema)]
pub fn to_schema_derive(input: TokenStream) -> TokenStream {
    // Construct a representation of Rust code as a syntax tree
    // that we can manipulate
    let ast = syn::parse(input).unwrap();

    // Build the trait implementation
    impl_to_schema(&ast)
}
