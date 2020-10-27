jQuery('<div class="pikachu-nav"><div class="pikachu-button pikachu-up">+</div><div class="pikachu-button pikachu-down">-</div></div>').insertAfter('.pikachu input');
jQuery('.pikachu').each(function() {
      var spinner = jQuery(this),
        input = spinner.find('input[type="number"]'),
          btnUp = spinner.find('.pikachu-up'),
          btnDown = spinner.find('.pikachu-down'),
        min = input.attr('min'),
        max = input.attr('max');

      btnUp.click(function() {
        var oldValue = parseFloat(input.val());
        if (oldValue >= max) {
          var newVal = oldValue;
        } else {
          var newVal = oldValue + 1;
        }
        spinner.find("input").val(newVal);
        spinner.find("input").trigger("change");
      });

      btnDown.click(function() {
        var oldValue = parseFloat(input.val());
        if (oldValue <= min) {
          var newVal = oldValue;
        } else {
          var newVal = oldValue - 1;
        }
        spinner.find("input").val(newVal);
        spinner.find("input").trigger("change");
      });

    });