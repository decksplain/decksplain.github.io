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

  RQRCODE_SVG_OPTIONS = {
    viewbox: true,
    # offset is rqrcode's default module size of 11px multiplied by
    # the recommended 'quiet area' for a QR code of four modules
    use_path: true
  }.freeze

  def initialize(tag_name, text, tokens)
    super
    @text = text
  end

  def render(context)
    resolved_text = Liquid::Template.parse(@text.strip).render(context)
    qr = RQRCode::QRCode.new(resolved_text, **RQRCODE_OPTIONS)
    qr.as_svg(**RQRCODE_SVG_OPTIONS).to_s
  end

  Liquid::Template.register_tag 'qr', self
end
