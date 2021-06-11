// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener('DOMContentLoaded', () => {
    const SELECT_FORMS_OPTIONS: Partial<M.FormSelectOptions> = {
        dropdownOptions: {
            container: document.body
        }
    }
    
    let AddGame: Vue = new Vue({
        el: "#add-game",
        data: {
            maps: [],
            heroes: [],
            roles: [],
            selectedRole: -1,
        },
        async created() {
            fetch('AddGame/MapList').then((response) => {
                response.json().then((data) => {
                    Vue.set(AddGame, "maps", data);
                    this.$nextTick(() => {
                        let select:HTMLSelectElement = <HTMLSelectElement>document.getElementById("Game_Map");
                        M.FormSelect.getInstance(select)?.destroy();
                        console.log(M.FormSelect.init(select, SELECT_FORMS_OPTIONS));
                    });
                })
            })
            
            fetch('AddGame/RoleList').then((response) => {
                response.json().then((data) => {
                    Vue.set(AddGame, "roles", data);
                    this.$nextTick(() => {
                        let select:HTMLSelectElement = <HTMLSelectElement>document.getElementById("Game_Role");
                        M.FormSelect.getInstance(select)?.destroy();
                        M.FormSelect.init(select, SELECT_FORMS_OPTIONS);
                    });
                })
            })
        },
        methods: {
            async updateHeroList() {
                fetch(`AddGame/HeroList?roleId=${AddGame.$data.selectedRole}`).then((response) => {
                    response.json().then((data) => {
                        Vue.set(AddGame, "heroes", data);
                        this.$nextTick(() => {
                            let select:HTMLSelectElement = <HTMLSelectElement>document.getElementById("Game_Heroes");
                            M.FormSelect.getInstance(select)?.destroy();
                            M.FormSelect.init(select, SELECT_FORMS_OPTIONS);
                        })
                    })
                })
            }
        }
    });

    const MODAL_OPTIONS: Partial<M.ModalOptions> = {
        onOpenEnd: function (el) {
            $(el).find('select2').trigger('select2:resize')
        }
    }
    let modals: NodeListOf<Element> = document.querySelectorAll('.modal')
    let modalInstances: M.Modal[] = M.Modal.init(modals, MODAL_OPTIONS);
    
    $('.select2').select2();
})