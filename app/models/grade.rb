class Grade < ApplicationRecord

  has_and_belongs_to_many :schools
  #TODO: has_many :enroollments

end
