package com.sdl.webapp.smarttarget.markup;

import com.sdl.webapp.common.api.WebRequestContext;
import com.sdl.webapp.common.api.model.ViewModel;
import com.sdl.webapp.common.markup.MarkupDecorator;
import com.sdl.webapp.common.markup.html.HtmlNode;
import com.sdl.webapp.common.markup.html.builders.HtmlBuilders;
import com.sdl.webapp.smarttarget.model.SmartTargetRegion;

/**
 * SmartTarget Region XPM Markup
 *
 * @author nic
 */
public class SmartTargetRegionXpmMarkup implements MarkupDecorator {

    @Override
    public HtmlNode process(HtmlNode markup, ViewModel model, WebRequestContext webRequestContext) {

        if ( webRequestContext.isPreview() ) {
            if (model instanceof SmartTargetRegion) {
                SmartTargetRegion stRegion = (SmartTargetRegion) model;

                markup =
                        HtmlBuilders.span()
                                .withLiteralContent(stRegion.getXpmMarkup())
                                .withContent(markup).build();

                /* NEEDED???
                markup = HtmlBuilders.span()
                        .withLiteralContent("<!-- Start Promotion Region: {\"RegionID\": \"" + stRegion.getName() + "\"} -->")
                        .withContent(markup).build();
                        */
            }
        }
        return markup;
    }

    @Override
    public int getPriority() {
        return 0;
    }
}