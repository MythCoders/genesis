class CustomFieldEnumeration < ActiveRecord::Base
  belongs_to :custom_field
  attr_accessible :name, :active, :position

  validates_presence_of :name, :position, :custom_field_id
  validates_length_of :name, :maximum => 60
  validates_numericality_of :position, :only_integer => true
  before_create :set_position

  scope :active, lambda { where(:active => true) }


end
