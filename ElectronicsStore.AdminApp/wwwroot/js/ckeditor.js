var ckeditor = function () {

    this.initialize = function () {

        registerEvents();

    };
    function registerEvents() {

        ClassicEditor.create(document.querySelector('#txt_description'), {}).then(editor => {
            window.editor = editor;
        }).catch(err => {
            console.error(err.stack)
        });
    }
};