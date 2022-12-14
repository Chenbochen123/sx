$(document).ready(function () {
    /* This code is executed after the DOM has been completely loaded */

    var tmp;

    $('.note').each(function () {
        /* Finding the biggest z-index value of the notes */
        tmp = $(this).css('z-index');
        if (tmp > zIndex) zIndex = tmp;
    })




    /*删除任务提醒的方法*/
    $('.delbtn').click(
       function () {
           $(this).parent().hide();
           $.get('TaskDelete.aspx', { id: $(this).attr('noteid')
           });
       }
    )





    /* A helper function for converting a set of elements to draggables: */
    make_draggable($('.note'));

    /* Configuring the fancybox plugin for the "Add a note" button: */
    $("#addButton").fancybox({
        'zoomSpeedIn': 600,
        'zoomSpeedOut': 500,
        'easingIn': 'easeOutBack',
        'easingOut': 'easeInBack',
        'hideOnContentClick': false,
        'padding': 15
    });



    $('#editNote').fancybox();




    /* Listening for keyup events on fields of the "Add a note" form: */
    $('.pr-body,.pr-author').live('keyup', function (e) {
        if (!this.preview)
            this.preview = $('#fancy_ajax .note');

        /* Setting the text of the preview to the contents of the input field, and stripping all the HTML tags: */
        this.preview.find($(this).attr('class').replace('pr-', '.')).html($(this).val().replace(/<[^>]+>/ig, ''));
    });

    /* Changing the color of the preview note: */
    $('.color').live('click', function () {
        $('#fancy_ajax .note').removeClass('yellow green blue').addClass($(this).attr('class').replace('color', ''));
    });

    /* The submit button: */
    $('#note-submit').live('click', function (e) {
        if ($('.pr-title').val().length < 1) {
            alert("请输入提醒标题!")
            return false;
        }
        if ($('.pr-title').val().length > 10) {
            alert("提醒标题超出规定长度!")
            return false;
        }
        if ($('.pr-body').val().length < 4) {
            alert("您输入的提醒内容太短!")
            return false;
        }
        if ($('.pr-body').val().length > 40) {
            alert("提醒内容超出规定长度!")
            return false;
        }
        $(this).replaceWith('<img src="img/ajax_load.gif" style="margin:30px auto;display:block" />');

        var data = {
            'zindex': ++zIndex,
            'title': $('.pr-title').val(),
            'body': $('.pr-body').val(),
            'color': $.trim($('#fancy_ajax .note').attr('class').replace('note', ''))
        };
        /* Sending an AJAX POST request: */
        $.post('TaskAdd.aspx', data, function (msg) {

            if (parseInt(msg)) {
                /* msg contains the ID of the note, assigned by MySQL's auto increment: */

                var tmp = $('#fancy_ajax .note').clone();

                tmp.find('span.data').text(msg).end().css({ 'z-index': zIndex, top: parseInt(Math.random() * 300), left: parseInt(Math.random() * 600) });
                tmp.appendTo($('#main'));

                make_draggable(tmp)
            }

            $("#addButton").fancybox.close();
        });

        e.preventDefault();
        var tab = parent.App.mainTabPanel.getActiveTab();
        if (tab) {
            tab.reload(true);
            //var html = tab.getBody().dom.innerHTML;
            //tab.getBody().update(html);
        }
    })

    $('.note-form').live('submit', function (e) { e.preventDefault(); });
});





var zIndex = 0;

function make_draggable(elements)
{
	/* Elements is a jquery object: */
	
	elements.draggable({
		containment:'parent',
		start:function(e,ui){ ui.helper.css('z-index',++zIndex); },
		stop:function(e,ui){
			
			/* Sending the z-index and positon of the note to update_position.php via AJAX GET: */

			$.get('TaskPosition.aspx',{
				  x		: ui.position.left,
				  y		: ui.position.top,
				  z		: zIndex,
				  id	: parseInt(ui.helper.find('span.eventId').html())
			});
		}
	});
}