// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener('DOMContentLoaded', () => {
    let AddGame: Vue = new Vue({
        el: "#add-game",
        data: {
            maps: [],
            heroes: [],
            
        },
        mounted: () => {
            //AJAX ici
        }
    });

    const MODAL_OPTIONS: Partial<M.ModalOptions> = {
        onOpenEnd: function (el) {
            $(el).find('select2').trigger('select2:resize')
        }
    }
    let modals: NodeListOf<Element> = document.querySelectorAll('.modal')
    let modalInstances: M.Modal[] = M.Modal.init(modals, MODAL_OPTIONS);

    const SELECT_FORMS_OPTIONS: Partial<M.FormSelectOptions> = {
        dropdownOptions: {
            container: document.body
        }
    }

    let selects: NodeListOf<Element> = document.querySelectorAll('select:not(.select2)');
    let selectInstances: M.FormSelect[] = M.FormSelect.init(selects, SELECT_FORMS_OPTIONS);
})