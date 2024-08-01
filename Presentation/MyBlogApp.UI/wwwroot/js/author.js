$(document).ready(function () {
    $('#create_author').on('click', function () {
        
        const _name = $('#name').val();
        const _imageUrl = $('#image_url').val();

        author.createAuthor({ name: _name, imageUrl: _imageUrl });
    });
});

const author = {
    createAuthor: function ({ name = '', imageUrl = '' }) {

        const _url = $('#create_author_form').attr('action');

        const _data = {
            name,
            imageUrl
        };

        $.ajax({
            url: _url,
            data: _data,
            type: 'POST',
            success: function (response) {
                $('#name').val('');
            },
            error: function (response) {
                console.log(response);
            }
        });
    }
}