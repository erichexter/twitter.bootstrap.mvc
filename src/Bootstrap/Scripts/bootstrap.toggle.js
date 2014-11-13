

$(document).ready(function() {

    var onclick = function (e) {
        this.nextSibling.checked = !this.nextSibling.checked;
        if (this.nextSibling.checked) {
            this.className += " "+ this.getAttribute("bshelper_act");
            
        } else {

            var pos = this.className.indexOf(this.getAttribute("bshelper_act") );
            if (pos != -1) {
                var posEnd = this.className.indexOf(" ", pos + 1);
                var newClassName = "";
                if (pos != 0) {
                    newClassName+= this.className.substring(0, pos - 1);
                }
                if (posEnd != -1) {
                    newClassName += this.className.substring(posEnd+1, this.className.length );
                }
                this.className = newClassName;
            }
            
        }
    };

    var j2 = $('div[bshelper="toggle"]');

    $('div[bshelper="toggle"]').bind("click", onclick );
    $('input[bshelper="toggle"]').each(function() {

    });

});



