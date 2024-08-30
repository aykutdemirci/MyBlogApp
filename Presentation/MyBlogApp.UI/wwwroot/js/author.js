$(document).ready(function () {
    $('#create_author').on('click', function () {
        
        const _name = $('#name').val();
        const _image = $('#image_url').prop('files')[0];

        author.createAuthor({ name: _name, image: _image });
    });
});

const author = {
    createAuthor: function ({ name = '', image = null }) {

        const _url = $('#create_author_form').attr('action');

        const formData = new FormData();
        formData.append('name', name);
        formData.append('image', image);

        $.ajax({
            url: _url,
            data: formData,
            type: 'POST',
            processData: false,
            contentType: false,
            success: function (response) {
                $('#name').val('');
            },
            error: function (response) {
                alert(response);
            }
        });
    }
}