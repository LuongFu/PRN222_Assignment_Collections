var modal = new bootstrap.Modal(document.getElementById('newsModal'));
var editor;

// Init CKEditor
ClassicEditor
    .create(document.querySelector('#NewsContent'))
    .then(e => editor = e);

function openCreateModal() {

    document.getElementById('modalTitle').innerText = "Create News";
    document.getElementById('newsForm').action = '/NewsArticles/Create';

    document.getElementById('NewsArticleId').value = '';
    document.getElementById('NewsTitle').value = '';
    document.getElementById('Headline').value = '';
    document.getElementById('NewsSource').value = '';
    document.getElementById('CategoryId').value = '';

    editor.setData('');

    modal.show();
}

function openEditModal(id, title, headline, content, source, categoryId) {

    document.getElementById('modalTitle').innerText = "Edit News";
    document.getElementById('newsForm').action = '/NewsArticles/Edit/' + id;

    document.getElementById('NewsArticleId').value = id;
    document.getElementById('NewsTitle').value = title;
    document.getElementById('Headline').value = headline;
    document.getElementById('NewsSource').value = source;
    document.getElementById('CategoryId').value = categoryId;

    editor.setData(content);

    modal.show();
}