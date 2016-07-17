class User < ApplicationRecord
  include ApplicationHelper

  # Include default devise modules. Others available are: :omniauthable
  devise :database_authenticatable, :registerable,
         :recoverable, :rememberable, :trackable, :validatable,
         :confirmable, :lockable, :timeoutable

  def full_name
    format_person_name(self.first_name, self.middle_name, self.last_name)
  end

  def is_admin?
    true
  end

end
