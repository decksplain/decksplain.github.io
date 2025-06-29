# SPDX-License-Identifier: GPL-3.0-only
# SPDX-FileCopyrightText: 2024 Sebastian Crane <seabass-labrax@gmx.com>

# frozen_string_literal: true

require 'erb'
require 'jekyll'
require 'rqrcode'

# Modified from https://codeberg.org/seabass/jekyll-qr/src/branch/dev/lib/jekyll-qr.rb to work with variables.
# Main tag definition for Jekyll Liquid
class QR < Liquid::Tag
  RQRCODE_OPTIONS = {
    level: :l
  }.freeze


  def initialize(tag_name, text, tokens)
    super
    @qr_text, @style = text.strip.split(' ', 2)
  end

  def render(context)
    # resolved_text = Liquid::Template.parse(@qr_text.strip).render(context)
    resolved_text = context[@qr_text] || context.dig(*@qr_text.split('.'))
    qr = RQRCode::QRCode.new(resolved_text, **RQRCODE_OPTIONS)

    rqrcode_svg_options = {
      viewbox: true,
      # offset is rqrcode's default module size of 10px multiplied by
      # the recommended 'quiet area' for a QR code of four modules
      use_path: true
    }

    if @style
      rqrcode_svg_options[:svg_attributes] = { style: @style }
    end

    qr.as_svg(rqrcode_svg_options).to_s
  end

  Liquid::Template.register_tag 'qr', self
end
