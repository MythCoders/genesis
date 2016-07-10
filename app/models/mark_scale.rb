class MarkScale < ApplicationRecord

  has_many :marks, :dependent => :delete_all
  #TODO: has_and_belongs_to_many :schools

end