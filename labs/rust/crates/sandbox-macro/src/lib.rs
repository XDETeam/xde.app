extern crate proc_macro;

use xde_sandbox::GivenStatement;

// This is a sample of a macro for custom structure parsing
// It can be used as
// ```
// bdd_macro!(Given Point);
//
// fn main() {
//    test_bdd_macro();
// }
// ```
#[proc_macro]
pub fn bdd_macro(input: proc_macro::TokenStream) -> proc_macro::TokenStream {
    let GivenStatement {
        object,
    } = syn::parse_macro_input!(input);

    proc_macro::TokenStream::from(quote::quote! {
        fn test_bdd_macro() {
            println!("object = {}", stringify!(#object));
        }
    })
}
